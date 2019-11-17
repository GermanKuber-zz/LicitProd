using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicitProd.Entities
{
    public class Response<TResponse>
    {
        public bool SuccessResult { get; }
        public List<string> Errors { get; set; } = new List<string>();
        public TResponse Result { get; set; }
        private int _usuarioId;
        public Usuario Usuario { get; set; } = new Usuario();
        public int UsuarioId
        {
            get
            {
                if (Usuario != null)
                    return Usuario.Id;
                return _usuarioId;
            }
            set => _usuarioId = value;
        }

        public Response<TResponse> Success(Func<TResponse, Task> callback)
        {
            if (SuccessResult)
            {
                Task.Run(async () =>
                {
                    await callback(Result);
                }).Wait();
            }

            return this;
        }
        public static Response<List<TResponse>> From(List<TResponse> values)
        {
            if (!values.Any())
                return new Response<List<TResponse>>(new List<string>());
            return new Response<List<TResponse>>(values);
        }
        public static Response<TResponse> From(TResponse value)
        {
            if (value == null)
                return new Response<TResponse>(new List<string>());
            return new Response<TResponse>(value);
        }
        public static Response<TResponse> From(Func<bool> condition, TResponse response)
        {
            if (condition())
                return new Response<TResponse>(response);
            return new Response<TResponse>(new List<string>());
        }
        public Response<TResponse> Success(Action<TResponse> callback)
        {
            if (SuccessResult)
                callback(Result);
            return this;
        }
        public TResult Success<TResult>(Func<TResponse, Task<TResult>> callback)
        {
            if (SuccessResult)
            {
                var returnValue = default(TResult);
                Task.Run(async () =>
                {
                    returnValue = await callback(Result);
                }).Wait();
                return returnValue;
            }
            return default(TResult);
        }
        public TResult Success<TResult>(Func<TResponse, TResult> callback, Func<TResult> errorDefaultValueCallback = null)
        {
            if (SuccessResult)
                return callback(Result);
            else
                if (errorDefaultValueCallback != null)
                return errorDefaultValueCallback.Invoke();
            else
                return default(TResult);
        }

        public Response<TResult> Success2<TResult>(Func<TResponse, Task<Response<TResult>>> callback, Func<Response<TResult>> errorDefaultValueCallback = null)
        {
            if (SuccessResult)
            {
                var returnValue = default(Response<TResult>);
                Task.Run(async () =>
                {
                    returnValue = await callback(Result);
                }).Wait();
                return returnValue;
            }
            else
            if (errorDefaultValueCallback != null)
                return errorDefaultValueCallback.Invoke();
            else
                return Response<TResult>.Error(Errors);
        }

        public TResult Map<TResult>(Func<TResponse, TResult> callbackSuccess, Func<List<string>, TResult> callbackError)
        {
            if (SuccessResult)
                return callbackSuccess(Result);
            return callbackError(Errors);
        }
        public TResult Map<TResult>(Func<TResponse, Task<TResult>> callbackSuccess, Func<List<string>, TResult> callbackError)
        {
            if (SuccessResult)
            {
                var returnValue = default(TResult);
                Task.Run(async () =>
                {
                    returnValue = await callbackSuccess(Result); ;
                }).Wait();
                return returnValue;

            }


            return callbackError(Errors);
        }

        public Response<TResponse> Error(Func<List<string>, Task> callback)
        {
            if (!SuccessResult)
                callback(Errors).Wait();
            return this;
        }
        public Response<TResponse> Error(Action<List<string>> callback)
        {
            if (!SuccessResult)
                callback(Errors);
            return this;
        }

        public static Response<TResponse> Error(List<string> errors) =>
            new Response<TResponse>(errors);
        public static Response<TResponse> Error() =>
            new Response<TResponse>(new List<string>());

        public static Response<TResponse> Ok(TResponse response) =>
            new Response<TResponse>(response);
        public static Response<TResponse> Error(string error) =>
            new Response<TResponse>(new List<string> { error });

        protected Response(TResponse response)
        {
            SuccessResult = true;
            Result = response;
        }


        protected Response(List<string> errors)
        {
            SuccessResult = false;
            Errors = new List<string>();

            Errors.AddRange(errors);
        }
    }

}
