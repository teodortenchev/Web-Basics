using SIS.HTTP.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Sessions
{
    public class HttpSession : IHttpSession
    {
        private Dictionary<string, object> sessionParameters;
        public string Id { get; }

        public HttpSession(string id)
        {
            CoreValidator.ThrowIfNullOrEmpty(id, nameof(id));

            Id = id;
            sessionParameters = new Dictionary<string, object>();
        }


        public void AddParameter(string name, object parameter)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));
            CoreValidator.ThrowIfNull(parameter, nameof(parameter));

            sessionParameters["name"] = parameter;
        }

        public void ClearParameters()
        {
            sessionParameters.Clear();
        }

        public bool ContainsParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            return sessionParameters.ContainsKey(name);
        }

        public object GetParameter(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            if(sessionParameters.ContainsKey(name))
            {
                return sessionParameters[name];
            }

            throw new InvalidOperationException("Session does not exist: " + name);
        }
    }
}
