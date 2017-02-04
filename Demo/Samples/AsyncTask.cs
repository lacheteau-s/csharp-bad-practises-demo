using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace Demo.Samples
{
    /// <summary>
    /// This is a readable version of what Roslyn generates when declaring an async Task function
    /// </summary>
    class AsyncTaskSample
    {
        public void EntryPoint(object sender, RoutedEventArgs e)
        {
            try
            {
                Task task = ThrowTaskExceptionAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [AsyncStateMachine(typeof(StateMachine))]
        private /* async */ Task ThrowTaskExceptionAsync()
        {
            StateMachine stateMachine = new StateMachine();
            stateMachine.This = this;
            stateMachine.Builder = AsyncTaskMethodBuilder.Create();
            stateMachine.State = -1;
            AsyncTaskMethodBuilder builder = stateMachine.Builder;
            builder.Start(ref stateMachine);
            return stateMachine.Builder.Task;
        }

        private sealed class StateMachine : IAsyncStateMachine
        {
            public int State;
            public AsyncTaskMethodBuilder Builder;
            public AsyncTaskSample This;

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
