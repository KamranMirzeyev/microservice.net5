﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Shared.DTO
{
    public class Responce<T>
    {
        public T Data { get; private set; }
        
        [JsonIgnore] 
        public int StatusCode { get; private set; }

        [JsonIgnore]
        public bool IsSuccess { get; private set; }

        public List<string> Errors { get; set; }

        //static factory method
        public static Responce<T> Success(T data, int statusCode)
        {
            return new Responce<T> {Data = data, StatusCode = statusCode, IsSuccess = true};
        }
        
        public static Responce<T> Success(int statusCode)
        {
            return new Responce<T> {Data = default(T), StatusCode = statusCode, IsSuccess = true};
        }

        public static Responce<T> Fail(List<string> errors, int statusCode)
        {
            return new Responce<T> {Errors = errors, StatusCode = statusCode, IsSuccess = false};
        }

        public static Responce<T> Fail(string error, int statusCode)
        {
            return new Responce<T> {Errors = new List<string>() {error}, StatusCode = statusCode, IsSuccess = false};
        }
    }
}