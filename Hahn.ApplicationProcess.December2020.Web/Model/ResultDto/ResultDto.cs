using System.Collections;
using System.Collections.Generic;

namespace Hahn.ApplicationProcess.December2020.Web.Model.ResultDto
{
    public class ResultDto<T> 
    {
        private ResultDto(IEnumerable<string> errors)
        {
            Errors = errors;
            Successful = false;
        }

        private ResultDto(T data)
        {
            Data = data;
            Successful = true; 
        }

        public T Data { get; private set; }
        public IEnumerable<string> Errors { get; private set;  }
        public bool Successful { get; private set;  }

        public static ResultDto<T> CreateSuccessfulResult<T>(T data)
        {
            return new ResultDto<T>(data); 
        }
        
        
        public static ResultDto<T> CreateFailedResult(IEnumerable<string> errors)
        {
            return new ResultDto<T>(errors); 
        }
    }
}