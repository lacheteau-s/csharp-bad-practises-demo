using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Demo.Samples
{
    /// <summary>
    /// This is a readable version of what Roslyn generates when declaring an awaited async void function
    /// </summary>
    class AwaitAsyncTaskSample
    {    
        [AsyncStateMachine(typeof(MainStateMachine))]
        public /* async */ void EntryPoint(object sender, RoutedEventArgs e)
        {
            MainStateMachine stateMachine = new MainStateMachine();
            stateMachine.This = this;
            stateMachine.Sender = sender;
            stateMachine.Builder = AsyncVoidMethodBuilder.Create();
            stateMachine.State = -1;
            AsyncVoidMethodBuilder builder = stateMachine.Builder;
            builder.Start(ref stateMachine);
        }

        private sealed class MainStateMachine : IAsyncStateMachine
        {
            public int State;
            public AsyncVoidMethodBuilder Builder;
            public object Sender;
            public AwaitAsyncTaskSample This;

            private Task _task;
            private Exception _ex;
            private TaskAwaiter _awaiter;

            void IAsyncStateMachine.MoveNext()
            {
                int num = State;
                try
                {
                    if (num != 0)
                    {
                    }
                    try
                    {
                        TaskAwaiter taskAwaiter;

                        if (num != 0)
                        {
                            _task = This.ThrowTaskExceptionAsync();
                            taskAwaiter = _task.GetAwaiter();

                            if (!taskAwaiter.IsCompleted)
                            {
                                State = 0;
                                _awaiter = taskAwaiter;
                                MainStateMachine stateMachine = this;
                                Builder.AwaitUnsafeOnCompleted(ref taskAwaiter, ref stateMachine);
                                return;
                            }
                        }
                        else
                        {
                            taskAwaiter = _awaiter;
                            _awaiter = default(TaskAwaiter);
                            State = -1;
                        }
                        taskAwaiter.GetResult();
                        taskAwaiter = default(TaskAwaiter);
                        _task = null;
                    }
                    catch (Exception exception)
                    {
                        _ex = exception;
                        MessageBox.Show(_ex.Message);
                    }
                }
                catch (Exception exception)
                {
                    State = -2;
                    Builder.SetException(exception);
                    return;
                }
                State = -2;
                Builder.SetResult();
            }

            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }

        [AsyncStateMachine(typeof(SubStateMachine))]
        private /* async */ Task ThrowTaskExceptionAsync()
        {
            SubStateMachine stateMachine = new SubStateMachine();
            stateMachine.This = this;
            stateMachine.Builder = AsyncTaskMethodBuilder.Create();
            stateMachine.State = -1;
            AsyncTaskMethodBuilder builder = stateMachine.Builder;
            builder.Start(ref stateMachine);
            return stateMachine.Builder.Task;
        }

        private sealed class SubStateMachine : IAsyncStateMachine
        {
            public int State;
            public AsyncTaskMethodBuilder Builder;
            public AwaitAsyncTaskSample This;

            void IAsyncStateMachine.MoveNext()
            {
                int num = State;

                try
                {
                    Console.WriteLine("Hello world!");
                    throw new InvalidOperationException("Exception thrown from an async Task returning method");
                }
                catch (Exception exception)
                {
                    State = -2;
                    Builder.SetException(exception);
                }
            }

            void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
            {
            }
        }
    }
}
