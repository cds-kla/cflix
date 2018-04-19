using Novell.Directory.Ldap;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace CFlix.ActiveDirectory.NETStandard
{
    public class ActiveDirectory : IDisposable
    {
        private LdapConnection connection;
        private readonly Uri adUri;

        public ActiveDirectory(string activeDirectoryUri)
            :this( string.IsNullOrEmpty(activeDirectoryUri)? null : new Uri(activeDirectoryUri))
        {
        }

        public ActiveDirectory(Uri activeDirectoryUri)
        {
            connection = new LdapConnection();
            adUri = activeDirectoryUri;
        }

        public ActiveDirectoryUser AuthenticateUser(string username, string password)
        {
            connection.Connect(adUri.Host, adUri.Port);

            try
            {
                connection.Bind(username, password);

                return SearchUser(username);
            }
            catch (LdapException e) when (e.ResultCode == LdapException.INVALID_CREDENTIALS)
            {
                return null;
            }
            finally
            {
                password = null;
                connection.Disconnect();
            }
        }

        private ActiveDirectoryUser SearchUser(string username)
        {
            var currentUser = new ActiveDirectoryUser();
            
            var lsc = connection.Search(
                adUri.AbsolutePath.Substring(1),
                LdapConnection.SCOPE_SUB,
                $"(sAMAccountName={(username.Contains("\\") ? username.Split('\\').Last() : username)})",
                new[] { "name", "userPrincipalName", "sAMAccountName", "employeeID", "extensionAttribute1" },
                false);

            while (lsc.hasMore())
            {
                LdapEntry nextEntry = null;
                try
                {
                    nextEntry = lsc.next();
                }
                catch (LdapException)
                {
                    continue;
                }

                currentUser.Name = nextEntry.getAttribute("name")?.StringValue;
                currentUser.Email = nextEntry.getAttribute("userPrincipalName")?.StringValue;
                currentUser.NameIdentifier = nextEntry.getAttribute("sAMAccountName")?.StringValue;
                currentUser.EmployeeID = nextEntry.getAttribute("employeeID")?.StringValue;

                return currentUser;
            }

            return null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    connection.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
