using System;
using System.Threading.Tasks;

namespace LicitProd.Mappers
{
    public static class AsyncHelper
    {

        public static TReturn CallAsyncMethod<TReturn>(Func<Task<TReturn>> callback)
        {
            var returnValue = default(TReturn);
            Task.Run(async () =>
            {
                returnValue = await callback();
            }).Wait();
            return returnValue;
        }
        public static void CallAsyncMethodVoid(Func<Task> callback)
        {
            Task.Run(async () =>
            {
                await callback();
            }).Wait();
        }
    }
}