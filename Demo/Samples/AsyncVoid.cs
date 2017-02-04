using System;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Demo.Samples
{
    /// <summary>
    /// This is a readable version of what Roslyn generates when declaring an async void function
    /// </summary>
    class AsyncVoidSample
    {
        public void EntryPoint(object sender, RoutedEventArgs e)
        {
            try
            {
                MyMethod();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        [AsyncStateMachine(typeof(StateMachine))]
        private /* async */ void MyMethod()
        {
            StateMachine stateMachine = new StateMachine();
            stateMachine.This = this;
            stateMachine.Builder = AsyncVoidMethodBuilder.Create();
            stateMachine.State = -1;
            AsyncVoidMethodBuilder builder = stateMachine.Builder;
            builder.Start(ref stateMachine);
        }

        private sealed class StateMachine : IAsyncStateMachine
        {
            public int State;
            public AsyncVoidMethodBuilder Builder;
            public AsyncVoidSample This;

            void IAsyncStateMachine.MoveNext()
            {
                try
                {
                    Console.WriteLine("Hello world!");
                    throw new InvalidOperationException("Exception thrown from an async void returning method");
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
    }
}
