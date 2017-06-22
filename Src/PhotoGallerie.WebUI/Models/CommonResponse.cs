﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoGalerie.Models
{
    public class CommonResponse
    {
        public CommonResponseStatus Status { get; set; }
        public object Data { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public CommonResponse() { }

        public CommonResponse(CommonResponseStatus status)
        {
            Status = status;
        }

        public static CommonResponse Success
        {
            get { return new CommonResponse(CommonResponseStatus.Success); }
        }

        public static CommonResponse Error
        {
            get { return new CommonResponse(CommonResponseStatus.Error); }
        }
    }

    public enum CommonResponseStatus
    {
        Unknown = 0,
        Success = 10,
        Error = 255
    }
}