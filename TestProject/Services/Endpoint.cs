using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Services
{
    public enum EndpointName
    {
        Posts,
        Comments,
        Users,
        Photos,
        Todos
    }

    public enum EndpointAction
    {
        None = 0,
        Edit,
        Get,
        Add,
    }

    public static class EnpointEnumExtensions
    {
        public static string Path(this EndpointName endpointName)
        {
            switch (endpointName)
            {
                case EndpointName.Posts:
                    return "posts";
                case EndpointName.Comments:
                    return "comments";
                case EndpointName.Users:
                    return "users";
                case EndpointName.Photos:
                    return "photos";
                case EndpointName.Todos:
                    return "todos";
                default:
                    return "";
            }
        }
    }


    public class Endpoint
    {
        public readonly EndpointName Name;
        public readonly EndpointAction Action;
        public long Id;

        public string BaseUrl;

        public Endpoint(EndpointName endpointName,
                        EndpointAction action = EndpointAction.None,
                        long id = 0)
        {
            Name = endpointName;
            Action = action;
            Id = id;
        }

        public Method Method
        {
            get
            {
                switch (Action)
                {
                    case EndpointAction.Add:
                        return Method.POST;

                    case EndpointAction.Edit:
                        return Method.PATCH;

                    case EndpointAction.Get:
                        return Method.GET;

                    default:
                        return Method.GET;
                }
            }
        }

        public string Url
        {
            get
            {
                var baseUrl = BaseUrl;

                return baseUrl + Path;
            }
        }

        private string Path
        {
            get
            {
                var path = Name.Path();
                return path;
            }
        }
    }
}
