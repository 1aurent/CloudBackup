/* ........................................................................
 * copyright 2015 Laurent Dupuis
 * ........................................................................
 * < This program is free software: you can redistribute it and/or modify
 * < it under the terms of the GNU General Public License as published by
 * < the Free Software Foundation, either version 3 of the License, or
 * < (at your option) any later version.
 * < 
 * < This program is distributed in the hope that it will be useful,
 * < but WITHOUT ANY WARRANTY; without even the implied warranty of
 * < MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * < GNU General Public License for more details.
 * < 
 * < You should have received a copy of the GNU General Public License
 * < along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * ........................................................................
 *
 */
using System;
using System.Collections;
using System.Configuration.Install;
using System.Reflection;
using System.ServiceProcess;

namespace CloudBackup
{
    partial class Program
    {
        private class InstallerOptions
        {
            public bool IsUser;
            public string UserName;
            public string Password;

            internal static InstallerOptions Parse(string[] args)
            {
                var options = new InstallerOptions();
                for (int index = 0; index < args.Length; index++)
                {
                    if (String.Compare(args[index], "-username", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        options.IsUser = true;
                        options.UserName = args[++index];
                    }
                    else if (String.Compare(args[index], "-password", StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        options.IsUser = true;
                        options.Password = args[++index];
                    }
                }
                return options;
            }
        }

        static void Install(bool install, InstallerOptions options)
        {
            var spi = new ServiceProcessInstaller();
            var si = new ServiceInstaller();
            spi.Account = ServiceAccount.NetworkService;
            if (options != null && options.IsUser)
            {
                spi.Account = ServiceAccount.User;
                if (options.UserName != null)
                {
                    spi.Username = options.UserName;
                }
                if (options.Password != null)
                {
                    spi.Password = options.Password;
                }
            }
            si.StartType = ServiceStartMode.Automatic;
            si.ServiceName = "CloudBackup";
            si.DisplayName = "Cloud Backup Service";
            si.Description = "Schedules, run and manage cloud backup";
            si.Parent = spi;

            string path = Assembly.GetEntryAssembly().Location;
            Console.WriteLine("Location : " + path);

            var ic = new InstallContext();
            ic.Parameters.Add("assemblypath", path);
            si.Context = ic;
            spi.Context = ic;

            IDictionary rb = install ? new Hashtable() : null;
            try
            {
                Console.WriteLine("Starting Default Installation");
                if (install)
                {
                    si.Install(rb);
                }
                else
                {
                    si.Uninstall(rb);
                }
            }
            catch (Exception ex)
            {
                log.Fatal(ex);
                if (rb != null)
                {
                    Console.WriteLine("Rollback Default Installation");
                    IDictionary rbc = rb;
                    rb = null;
                    si.Rollback(rbc);
                }
            }
            finally
            {
                if (rb != null)
                {
                    Console.WriteLine("Commit Default Installation");
                    si.Commit(rb);
                }
            }
        }

    }
}