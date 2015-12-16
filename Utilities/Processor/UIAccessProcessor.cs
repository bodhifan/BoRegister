using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;

namespace Utilities.Processor
{
   // 定义统一的访问接口，
   // 所有调用通过委托执行，解耦对UI的访问    
   public delegate bool AccessHandler(Object sender, Object param);

    // 日志记录
   public delegate bool LogHandler(Object sender, string param, LogType type);

    // 自定义访问类型
    public class HandlerNode
    {
       public HandlerNode previous;
       public HandlerNode next;
       public AccessHandler nodeHandler;
       public int nextTimeOuts; // 下一次执行的间隔时间
       public string handlerName;
       
        public HandlerNode()
        {
            previous = next = null;
            nodeHandler = null;
            nextTimeOuts = 1000; //默认间隔一秒
            handlerName = "undefined";
            
        }
        public HandlerNode(HandlerNode previous, AccessHandler cur,string handlerName, int timeOuts = 1000)
        {
            this.previous = previous;
            this.nodeHandler = cur;
            nextTimeOuts = timeOuts;
            this.handlerName = handlerName;
        
            // 与前一个关联
            this.previous.next = this;
        }
    }
    /// <summary>
    /// 网页UI访问处理器
    /// </summary>
    public class UIAccessProcessor
    {
        
        // 定义
        HandlerNode header;
        HandlerNode priviousNode;

        AccessHandler conditionNode; // 条件表达式
        AccessHandler preNode;   // 预处理
        AccessHandler postNode;  // 后处理
        AccessHandler nodeAtFirstTime; // 预先处理，在每一个node第一次调用时被触发
        string endExcuteNodeName = ""; // 一旦某个节点失败时，跳转的终止节点名称

        // 执行
        HandlerNode curExcute;// 指向当前执行的对象

        Dictionary<string, HandlerNode> handleMap = new Dictionary<string, HandlerNode>();

        // 同步事件
        AutoResetEvent resetEvent = new AutoResetEvent(false);
        Mutex mutex = new Mutex();
        // 停止处理
        bool IsStop;

        // 使用goto 的超时时间
        bool IsUsedGoto;
        int timeOutsOfGoto;

        // 每个任务的超时时间
        int HandlerTimeOuts;
        // 任务是否超时
        bool IsHandlerTimeOuts;
        bool IsHandleFinished;
        // 日志 
        LogHandler printLog;
        LogType currentLogType = LogType.Debug;
        public Form frmWindow = null;
        bool allFinished = false;

        bool isFirstTimeExcuteNode = true;
        int repeatTimes = 5;
        int currentRepeatTimes = 0;

        System.Timers.Timer timer;
        public UIAccessProcessor(LogHandler log)
        {
            header = new HandlerNode();
            priviousNode = header;
            printLog = log;
            IsStop = false;
            conditionNode = preNode = postNode = nodeAtFirstTime = null;
            HandlerTimeOuts = 5000;
        }

        private bool isAsyn = true;
        public void SetAsynCalling(bool mode)
        {
            isAsyn = mode;
        }
        public void Reset()
        {
            IsUsedGoto = false;
            isFirstTimeExcuteNode = true;
            conditionNode = null;
            preNode = null;
            postNode = null;
            nodeAtFirstTime = null;
            handleMap.Clear();
        }
        /// <summary>
        /// 添加处理单元
        /// </summary>
        /// <param name="handler"></param>
        /// <param name="name"></param>
        /// <param name="timeOuts"></param>
        /// <returns></returns>
        public bool AddHandler(AccessHandler handler, string name = null, int timeOuts = 1000)
        {
            MethodInfo method = handler.Method;
            string mName = name;
            if (name == null)
            {
                mName = method.Name;
            }
            if (handleMap.ContainsKey(mName))
            {
                PrintLog("已经具有同名函数", currentLogType);
                return false;
            }

            timeOuts += RandomGenerator.getRandom().Next(100);
            HandlerNode cur = new HandlerNode(priviousNode, handler, mName, timeOuts);
            handleMap.Add(mName, cur);
            priviousNode = cur;
          //  priviousNode.next = header.next;
            return true;
        }
        public void SetCondition(AccessHandler condition)
        {
            this.conditionNode = condition;
        }
        public void SetPreProcess(AccessHandler preHandler)
        {
            this.preNode = preHandler;
        }
        public void SetPostProcess(AccessHandler postHandler)
        {
            this.postNode = postHandler;
        }
        public void EndExcute(string endExcute,int times)
        {
            this.endExcuteNodeName = endExcute;
            repeatTimes = times;
        }
        public void SetProcessAtFirstTime(AccessHandler nodeAtFirst)
        {
            this.nodeAtFirstTime = nodeAtFirst;
        }
        public void MakeCycle()
        {
            priviousNode.next = header.next;
        }
        public bool AddHandler(AccessHandler handler, int timeOuts)
        {
            return AddHandler(handler, null, timeOuts);
        }

        /// <summary>
        /// 跳转到处理单元
        /// </summary>
        /// <param name="name"></param>
        /// <param name="timeOuts"></param>
        /// <returns></returns>
        private string gotoHandleName = "";
        public bool GotoHanlderUnit(string name, int timeOuts)
        {
            if (!handleMap.ContainsKey(name))
            {
                PrintLog("未找到相应的处理单元", currentLogType);
                return false;
            }
            gotoHandleName = name;
           // curExcute = curExcute.previous;
            timeOutsOfGoto = timeOuts;
            IsUsedGoto = true;
            isFirstTimeExcuteNode = true;
            return true;
        }
        public bool IsStoped()
        {
            return IsStop;
        }
        public int SetExcuteTimeOuts(int timeouts)
        {
            int temp = HandlerTimeOuts;
            this.HandlerTimeOuts = timeouts;
            return temp;
        }
        public void ResetRepeatTimes()
        {
            currentRepeatTimes = 0;
        }
        public void Stop()
        {
            if (!allFinished && !IsStop)
            {
                PrintLog("正在停止...");
                IsStop = true;
            }
            else
            {
                PrintLog("流程已经停止");
            }
        }
        /// <summary>
        /// 开始执行
        /// </summary>
        public void Excute()
        {
            IsStop = false;
            allFinished = false;
            // 初试变量
            curExcute = header.next;
            timer = new System.Timers.Timer(HandlerTimeOuts);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            while (!IsStop)
            {
                
                try
                {
                    if (conditionNode != null && !conditionNode.Invoke(null, null))
                    {
                            WaitForTimes(1000);
                            PrintLog("等待条件完成...");
                            continue;
                    }

                    WaitForTimes(500);

                    if (null == curExcute || curExcute.nodeHandler == null)
                    {
                        PrintLog("处理单元为空!");
                        break;
                    }
                   resetEvent.Reset();

                    // 等待                
                    if (IsUsedGoto)
                    {
                        curExcute = handleMap[gotoHandleName];
                        PrintLog("跳转到流程：" + curExcute.handlerName);
                        PrintLog("【"+curExcute.handlerName + "】开始： " + timeOutsOfGoto.ToString());
                        WaitForTimes(timeOutsOfGoto);
                        IsUsedGoto = false;
                    }
                    else
                    {
                        PrintLog("【"+curExcute.handlerName + "】开始： " + curExcute.nextTimeOuts.ToString());
                        WaitForTimes(curExcute.nextTimeOuts);
                    }
                    // 调用预处理
                    if (preNode != null)
                    {
                        preNode.Invoke(null, null);
                    }

                    // 如果是第一次执行该node,则调用FirstTimeExcute Node
                    if (isFirstTimeExcuteNode)
                    {
                        // 如果存在第一次执行节点时，需要执行的节点，调用该节点
                        if (nodeAtFirstTime != null)
                        {
                            nodeAtFirstTime.Invoke(null, null);
                        }
                        currentRepeatTimes = 0;
                    }
                    string currentHandlerName = curExcute.handlerName;
                    if (currentRepeatTimes >= repeatTimes && endExcuteNodeName != "")
                    {
                        PrintLog("【" + curExcute.handlerName + "】重复执行,超过 " + currentRepeatTimes.ToString()+" 次");
                        GotoHanlderUnit(endExcuteNodeName, 1000);
                        continue;
                    }
                    // 开始计时,如果任务在HandlerTimeOut时间间隔内没完成则超时
                    IsHandlerTimeOuts = false;
                    IsHandleFinished = false;
                    timer.Enabled = true;
                    timer.Start();
                    asyncResult = null;
                    if (isAsyn)
                        ExcuteProxy(curExcute);
                    else
                        ExcuteProxySyn(curExcute);

                    // 调用后处理
                    if (postNode != null)
                    {
                        postNode.Invoke(null, null);
                    }
                    resetEvent.WaitOne();
                    timer.Stop();
                    if (IsHandlerTimeOuts)
                    {
                        PrintLog("【" + currentHandlerName + "】 运行时间超时,关闭重来");
                        GotoHanlderUnit(endExcuteNodeName, 1000);
                        isFirstTimeExcuteNode = true;
                        currentRepeatTimes = 0;
                    }
                    else
                    {
                        PrintLog("【" + currentHandlerName + "】完成");
                    }
                }
                catch (System.Exception ex)
                {
                    PrintLog("出现异常..." + ex.StackTrace);
                }

            }
            allFinished = true;
            PrintLog("处理流程已经停止!");
        }
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            if (IsHandleFinished)
            {
                return;
            }

            IsHandlerTimeOuts = true;
            IsHandleFinished = true;
            if (asyncResult != null)
            {
                try
                {
                    currentThread.Abort();
                }
                catch
                {
                    PrintLog("强制停止任务失败!");
                }
                
            }
            PrintLog("【运行超时】");
            resetEvent.Set();

            
        }

        private void WaitForTimes(int times)
        {
            try
            {
                System.Threading.Thread.Sleep(times);
            }
            catch (System.Exception ex)
            {
                PrintLog("WaitForTimes 暂停执行失败," + ex.Message);
            }
        }
        private void HandlerCompleted(IAsyncResult asynResult)
        {
            IsHandleFinished = true;
            // 如果超时了，直接退出
            if (IsHandlerTimeOuts)
            {
                return;
            }
            if (IsStop)
            {
                (asynResult.AsyncState as HandlerNode).nodeHandler.EndInvoke(asynResult);
                resetEvent.Set();
                return;
            }
            if (asynResult == null)
            {
                return;
            }
           bool flag = (asynResult.AsyncState as HandlerNode).nodeHandler.EndInvoke(asynResult);
           if (flag == true)
           {
              // PrintLog("将当前执行单元指向下一个...");
               curExcute = curExcute.next;
               isFirstTimeExcuteNode = true;
               currentRepeatTimes = 0;
           }
           else
           {
               currentRepeatTimes++;
               isFirstTimeExcuteNode = false;
           }
          // PrintLog("子线程完成执行单元任务....");
           resetEvent.Set();
           
            
        }
        IAsyncResult asyncResult = null;
        Thread currentThread = null;
        private void ExcuteProxy(HandlerNode excutor)
        {
            currentThread = new Thread(new ThreadStart(delegate(){
                object obj = new object();
                asyncResult = excutor.nodeHandler.BeginInvoke(this, obj, HandlerCompleted, curExcute);
            }));
            currentThread.Start();
        }

        private void ExcuteProxySyn(HandlerNode excutor)
        {
           bool suc = excutor.nodeHandler.Invoke(this, null);
           if (suc)
           {
               PrintLog("将当前执行单元指向下一个...");
               curExcute = curExcute.next;
               resetEvent.Set();
           }
        }
        // 打印日志
        public void PrintLog(string log, LogType type = LogType.Debug)
        {
            printLog.Invoke(this,log, type);
        }
    }
}
