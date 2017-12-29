using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace WmsSDK
{
    public enum NCBCONST
    {
        NCBNAMSZ = 16,
        MAX_LANA = 254,
        NCBENUM = 0x37,
        NRC_GOODRET = 0x00,
        NCBRESET = 0x32,
        NCBASTAT = 0x33,
        NUM_NAMEBUF = 30,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ADAPTER_STATUS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
        public byte[] adapter_address;
        public byte rev_major;
        public byte reserved0;
        public byte adapter_type;
        public byte rev_minor;
        public ushort duration;
        public ushort frmr_recv;
        public ushort frmr_xmit;
        public ushort iframe_recv_err;
        public ushort xmit_aborts;
        public uint xmit_success;
        public uint recv_success;
        public ushort iframe_xmit_err;
        public ushort recv_buff_unavail;
        public ushort t1_timeouts;
        public ushort ti_timeouts;
        public uint reserved1;
        public ushort free_ncbs;
        public ushort max_cfg_ncbs;
        public ushort max_ncbs;
        public ushort xmit_buf_unavail;
        public ushort max_dgram_size;
        public ushort pending_sess;
        public ushort max_cfg_sess;
        public ushort max_sess;
        public ushort max_sess_pkt_size;
        public ushort name_count;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NAME_BUFFER
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
        public byte[] name;
        public byte name_num;
        public byte name_flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NCB
    {
        public byte ncb_command;
        public byte ncb_retcode;
        public byte ncb_lsn;
        public byte ncb_num;
        public IntPtr ncb_buffer;
        public ushort ncb_length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
        public byte[] ncb_callname;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NCBNAMSZ)]
        public byte[] ncb_name;
        public byte ncb_rto;
        public byte ncb_sto;
        public IntPtr ncb_post;
        public byte ncb_lana_num;
        public byte ncb_cmd_cplt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public byte[] ncb_reserve;
        public IntPtr ncb_event;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LANA_ENUM
    {
        public byte length;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.MAX_LANA)]
        public byte[] lana;
    }

    [StructLayout(LayoutKind.Auto)]
    public struct ASTAT
    {
        public ADAPTER_STATUS adapt;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)NCBCONST.NUM_NAMEBUF)]
        public NAME_BUFFER[] NameBuff;
    }
    public class Win32API
    {
        [DllImport("NETAPI32.DLL")]
        public static extern char Netbios(ref NCB ncb);
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class IP_Adapter_Addresses
    {
        public uint Length;
        public uint IfIndex;
        public IntPtr Next;

        public IntPtr AdapterName;
        public IntPtr FirstUnicastAddress;
        public IntPtr FirstAnycastAddress;
        public IntPtr FirstMulticastAddress;
        public IntPtr FirstDnsServerAddress;

        public IntPtr DnsSuffix;
        public IntPtr Description;

        public IntPtr FriendlyName;

        [MarshalAs(UnmanagedType.ByValArray,
             SizeConst = 8)]
        public Byte[] PhysicalAddress;

        public uint PhysicalAddressLength;
        public uint flags;
        public uint Mtu;
        public uint IfType;

        public uint OperStatus;

        public uint Ipv6IfIndex;
        public uint ZoneIndices;

        public IntPtr FirstPrefix;
    }

    /// <summary>
    /// 获取计算机信息
    /// </summary>
    public class ComputerHelper
    {
        /// <summary>
        /// 获取MAC地址(多方式保证)
        /// </summary>
        public string GetMacAddress()
        {
            string macAddress = string.Empty;
            try
            {
                macAddress = GetMacAddressByNetBios();
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(macAddress))
            {
                return macAddress;
            }
            try
            {
                macAddress = GetMacAddressByWMI();
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(macAddress))
            {
                return macAddress;
            }
            try
            {
                macAddress = GetMacAddressBySendARP();
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(macAddress))
            {
                return macAddress;
            }
            try
            {
                macAddress = GetMacAddressByAdapter();
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(macAddress))
            {
                return macAddress;
            }
            try
            {
                macAddress = GetMacAddressByDos();
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(macAddress))
            {
                return macAddress;
            }
            try
            {
                macAddress = GetMacAddressByNetworkInformation();
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(macAddress))
            {
                return macAddress;
            }
            return macAddress;
        }

        public static string GetCPUID()
        {
            try
            {
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();

                String strCpuID = null;
                foreach (ManagementObject mo in moc)
                {
                    strCpuID = mo.Properties["ProcessorId"].Value.ToString();
                    break;
                }
                return strCpuID;
            }
            catch
            {
                return Environment.MachineName;
            }

        }

        /// <summary>
        /// 通过WMI获得电脑的mac地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressByWMI()
        {
            string mac = "";
            try
            {
                ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
                ManagementObjectCollection queryCollection = query.Get();

                foreach (ManagementObject mo in queryCollection)
                {
                    if (mo["IPEnabled"].ToString() == "True")
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
            }
            return mac;
        }

        [DllImport("Iphlpapi.dll")]
        static extern int SendARP(Int32 DestIP, Int32 SrcIP, ref Int64 MacAddr, ref Int32 PhyAddrLen);
        /// <summary>
        /// SendArp获取MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressBySendARP()
        {
            StringBuilder strReturn = new StringBuilder();
            try
            {
                System.Net.IPHostEntry Tempaddr = (System.Net.IPHostEntry)Dns.GetHostByName(Dns.GetHostName());
                System.Net.IPAddress[] TempAd = Tempaddr.AddressList;
                Int32 remote = (int)TempAd[0].Address;
                Int64 macinfo = new Int64();
                Int32 length = 6;
                SendARP(remote, 0, ref macinfo, ref length);

                string temp = System.Convert.ToString(macinfo, 16).PadLeft(12, '0').ToUpper();

                int x = 12;
                for (int i = 0; i < 6; i++)
                {
                    if (i == 5) { strReturn.Append(temp.Substring(x - 2, 2)); }
                    else { strReturn.Append(temp.Substring(x - 2, 2) + ":"); }
                    x -= 2;
                }

                return strReturn.ToString();
            }
            catch
            {
                return "";
            }
        }

        [DllImport("Iphlpapi.dll")]
        public static extern uint GetAdaptersAddresses(uint Family, uint flags, IntPtr Reserved,
            IntPtr PAdaptersAddresses, ref uint pOutBufLen);

        /// <summary>
        /// 通过适配器信息获取MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressByAdapter()
        {
            string macAddress = "";
            try
            {
                IntPtr PAdaptersAddresses = new IntPtr();

                uint pOutLen = 100;
                PAdaptersAddresses = Marshal.AllocHGlobal(100);

                uint ret =
                    GetAdaptersAddresses(0, 0, (IntPtr)0, PAdaptersAddresses, ref pOutLen);

                if (ret == 111)
                {
                    Marshal.FreeHGlobal(PAdaptersAddresses);
                    PAdaptersAddresses = Marshal.AllocHGlobal((int)pOutLen);
                    ret = GetAdaptersAddresses(0, 0, (IntPtr)0, PAdaptersAddresses, ref pOutLen);
                }

                IP_Adapter_Addresses adds = new IP_Adapter_Addresses();

                IntPtr pTemp = PAdaptersAddresses;

                while (pTemp != (IntPtr)0)
                {
                    Marshal.PtrToStructure(pTemp, adds);
                    string adapterName = Marshal.PtrToStringAnsi(adds.AdapterName);
                    string FriendlyName = Marshal.PtrToStringAuto(adds.FriendlyName);
                    string tmpString = string.Empty;

                    for (int i = 0; i < 6; i++)
                    {
                        tmpString += string.Format("{0:X2}", adds.PhysicalAddress[i]);

                        if (i < 5)
                        {
                            tmpString += ":";
                        }
                    }


                    RegistryKey theLocalMachine = Registry.LocalMachine;

                    RegistryKey theSystem
                        = theLocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\Interfaces");
                    RegistryKey theInterfaceKey = theSystem.OpenSubKey(adapterName);

                    if (theInterfaceKey != null)
                    {
                        macAddress = tmpString;
                        break;
                    }

                    pTemp = adds.Next;
                }
            }
            catch
            { }
            return macAddress;
        }

        /// <summary>
        /// 通过NetBios获取MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressByNetBios()
        {
            string macAddress = "";
            try
            {
                string addr = "";
                int cb;
                ASTAT adapter;
                NCB Ncb = new NCB();
                char uRetCode;
                LANA_ENUM lenum;

                Ncb.ncb_command = (byte)NCBCONST.NCBENUM;
                cb = Marshal.SizeOf(typeof(LANA_ENUM));
                Ncb.ncb_buffer = Marshal.AllocHGlobal(cb);
                Ncb.ncb_length = (ushort)cb;
                uRetCode = Win32API.Netbios(ref Ncb);
                lenum = (LANA_ENUM)Marshal.PtrToStructure(Ncb.ncb_buffer, typeof(LANA_ENUM));
                Marshal.FreeHGlobal(Ncb.ncb_buffer);
                if (uRetCode != (short)NCBCONST.NRC_GOODRET)
                    return "";

                for (int i = 0; i < lenum.length; i++)
                {
                    Ncb.ncb_command = (byte)NCBCONST.NCBRESET;
                    Ncb.ncb_lana_num = lenum.lana[i];
                    uRetCode = Win32API.Netbios(ref Ncb);
                    if (uRetCode != (short)NCBCONST.NRC_GOODRET)
                        return "";

                    Ncb.ncb_command = (byte)NCBCONST.NCBASTAT;
                    Ncb.ncb_lana_num = lenum.lana[i];
                    Ncb.ncb_callname[0] = (byte)'*';
                    cb = Marshal.SizeOf(typeof(ADAPTER_STATUS)) + Marshal.SizeOf(typeof(NAME_BUFFER)) * (int)NCBCONST.NUM_NAMEBUF;
                    Ncb.ncb_buffer = Marshal.AllocHGlobal(cb);
                    Ncb.ncb_length = (ushort)cb;
                    uRetCode = Win32API.Netbios(ref Ncb);
                    adapter.adapt = (ADAPTER_STATUS)Marshal.PtrToStructure(Ncb.ncb_buffer, typeof(ADAPTER_STATUS));
                    Marshal.FreeHGlobal(Ncb.ncb_buffer);

                    if (uRetCode == (short)NCBCONST.NRC_GOODRET)
                    {
                        if (i > 0)
                            addr += ":";
                        addr = string.Format("{0,2:X}:{1,2:X}:{2,2:X}:{3,2:X}:{4,2:X}:{5,2:X}",
                              adapter.adapt.adapter_address[0],
                              adapter.adapt.adapter_address[1],
                              adapter.adapt.adapter_address[2],
                              adapter.adapt.adapter_address[3],
                              adapter.adapt.adapter_address[4],
                              adapter.adapt.adapter_address[5]);
                    }
                }
                macAddress = addr.Replace(' ', '0');

            }
            catch
            {

            }

            return macAddress;
        }

        /// <summary>
        /// 通过DOS命令获得MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressByDos()
        {
            string macAddress = "";
            Process p = null;
            StreamReader reader = null;
            try
            {
                ProcessStartInfo start = new ProcessStartInfo("cmd.exe");

                start.FileName = "ipconfig";
                start.Arguments = "/all";

                start.CreateNoWindow = true;

                start.RedirectStandardOutput = true;

                start.RedirectStandardInput = true;

                start.UseShellExecute = false;

                p = Process.Start(start);

                reader = p.StandardOutput;

                string line = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    if (line.ToLower().IndexOf("physical address") > 0 || line.ToLower().IndexOf("物理地址") > 0)
                    {
                        int index = line.IndexOf(":");
                        index += 2;
                        macAddress = line.Substring(index);
                        macAddress = macAddress.Replace('-', ':');
                        break;
                    }
                    line = reader.ReadLine();
                }
            }
            catch
            {

            }
            finally
            {
                if (p != null)
                {
                    p.WaitForExit();
                    p.Close();
                }
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return macAddress;
        }

        /// <summary>
        /// 通过网络适配器获取MAC地址
        /// </summary>
        /// <returns></returns>
        public string GetMacAddressByNetworkInformation()
        {
            string macAddress = "";
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (!adapter.GetPhysicalAddress().ToString().Equals(""))
                    {
                        macAddress = adapter.GetPhysicalAddress().ToString();
                        for (int i = 1; i < 6; i++)
                        {
                            macAddress = macAddress.Insert(3 * i - 1, ":");
                        }
                        break;
                    }
                }

            }
            catch
            {
            }
            return macAddress;
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            string ip = GetCurrentIP();
            if (!string.IsNullOrWhiteSpace(ip))
            {
                return ip;
            }
            try
            {
                ip = Dns.GetHostAddresses(Dns.GetHostName())[0].ToString();
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(ip))
            {
                return ip;
            }
            return ip;
        }

        private static string GetCurrentIP()
        {
            string ip = "";
            try
            {
                List<IPAddress> addresses = (from f in Dns.GetHostAddresses(Environment.MachineName)
                                             where f.AddressFamily == AddressFamily.InterNetwork
                                             select f).ToList();
                IPAddress ipAddress = addresses.FirstOrDefault(ipa => { return !ipa.ToString().StartsWith("169.254"); });
                if (ipAddress == null)
                {
                    ip = addresses.First().ToString();
                }
                else
                {
                    ip = ipAddress.ToString();
                }
            }
            catch { }
            return ip;
        }


        public static string GetOSName()
        {
            string osName = string.Empty;
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    if (queryObj == null)
                        continue;
                    osName = (queryObj["Caption"] ?? "").ToString() + " " + (queryObj["CSDVersion"] ?? "").ToString();
                }
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(osName))
            {
                return osName;
            }
            try
            {
                osName = new WinVersion().GetVersionName();
            }
            catch { }
            if (!string.IsNullOrWhiteSpace(osName))
            {
                return osName;
            }
            return osName;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class OSVERSIONINFO
    {
        public Int32 dwOSVersionInfoSize;
        public Int32 dwMajorVersion;
        public Int32 dwMinorVersion;
        public Int32 dwBuildNumber;
        public Int32 dwPlatformId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public String szCSDVersion;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class OSVERSIONINFOEX : OSVERSIONINFO
    {
        public Int16 wServicePackMajor;
        public Int16 wServicePackMinor;
        public Int16 wSuiteMask;
        public Byte wProductType;
        public Byte wReserved;
    }



    public class WinVersion
    {
        [DllImport("kernel32.dll")]
        public extern static Boolean GetVersionEx([In, Out] OSVERSIONINFO versionInfo);

        public const Int32 VER_PLATFORM_WIN32s = 0;
        public const Int32 VER_PLATFORM_WIN32_WINDOWS = 1;
        public const Int32 VER_PLATFORM_WIN32_NT = 2;

        public const Int32 VER_NT_WORKSTATION = 1;
        public const Int32 VER_NT_DOMAIN_CONTROLLER = 2;
        public const Int32 VER_NT_SERVER = 3;

        // Microsoft Small Business Server 
        public const Int32 VER_SUITE_SMALLBUSINESS = 1;
        // Win2k Adv Server or .Net Enterprise Server 
        public const Int32 VER_SUITE_ENTERPRISE = 2;
        // Terminal Services is installed.   
        public const Int32 VER_SUITE_TERMINAL = 16;
        // Win2k Datacenter 
        public const Int32 VER_SUITE_DATACENTER = 128;
        // Terminal server in remote admin mode 
        public const Int32 VER_SUITE_SINGLEUSERTS = 256;
        public const Int32 VER_SUITE_PERSONAL = 512;
        // Microsoft .Net webserver installed 
        public const Int32 VER_SUITE_BLADE = 1024;

        private OSVERSIONINFO versionInfo;

        public WinVersion()
        {

        }

        public String GetVersionName()
        {
            String name = String.Empty;
            Boolean success = true;
            Boolean bVersionInfoEx;

            versionInfo = new OSVERSIONINFOEX();
            versionInfo.dwOSVersionInfoSize = Marshal.SizeOf(versionInfo);
            bVersionInfoEx = GetVersionEx(versionInfo);

            if (!bVersionInfoEx)
            {
                versionInfo = new OSVERSIONINFO();
                versionInfo.dwOSVersionInfoSize = Marshal.SizeOf(versionInfo);
                success = GetVersionEx(versionInfo);

                if (!success)
                {
                    return "<获取失败>";
                }
            }

            switch (versionInfo.dwPlatformId)
            {
                // Win NT家族 
                case VER_PLATFORM_WIN32_NT:
                    if (versionInfo.dwMajorVersion == 5 &&
                        versionInfo.dwMinorVersion == 2)
                    {
                        name = "Microsoft Windows Server 2003, ";
                    }

                    if (versionInfo.dwMajorVersion == 5 &&
                        versionInfo.dwMinorVersion == 1)
                    {
                        name = "Microsoft Windows XP ";
                    }

                    if (versionInfo.dwMajorVersion == 5 &&
                        versionInfo.dwMinorVersion == 0)
                    {
                        name = "Microsoft Windows 2000 ";
                    }

                    if (versionInfo.dwMajorVersion == 6 &&
                        versionInfo.dwMinorVersion == 0)
                    {
                        name = "Microsoft Windows Vista ";
                    }

                    if (versionInfo.dwMajorVersion == 6 &&
                        versionInfo.dwMinorVersion == 1)
                    {
                        name = "Microsoft Windows 7 ";
                    }

                    if (versionInfo.dwMajorVersion == 6 &&
                        versionInfo.dwMinorVersion == 2)
                    {
                        name = "Microsoft Windows 8 ";
                    }

                    // 说明为Windows NT 4.0 SP6及更新的系统 
                    if (bVersionInfoEx)
                    {
                        // 工作站类型 
                        if (((OSVERSIONINFOEX)versionInfo).wProductType == VER_NT_WORKSTATION)
                        {
                            if (versionInfo.dwMajorVersion == 4)
                            {
                                name += "Workstation 4.0 ";
                            }
                            else if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_PERSONAL) == VER_SUITE_PERSONAL)
                            {
                                name += "Home Edition ";
                            }
                            else
                            {
                                name += "Professional Edition ";
                            }
                        }
                        // 服务器类型 
                        else if (((OSVERSIONINFOEX)versionInfo).wProductType == VER_NT_SERVER ||
                            ((OSVERSIONINFOEX)versionInfo).wProductType == VER_NT_DOMAIN_CONTROLLER)
                        {
                            if (versionInfo.dwMajorVersion == 5 &&
                                versionInfo.dwMinorVersion == 2)
                            {
                                if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_DATACENTER) == VER_SUITE_DATACENTER)
                                {
                                    name += "Datacenter Edition ";
                                }
                                else if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_ENTERPRISE) == VER_SUITE_ENTERPRISE)
                                {
                                    name += "Enterprise Edition ";
                                }
                                else if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_BLADE) == VER_SUITE_BLADE)
                                {
                                    name += "Web Edition ";
                                }
                                else
                                {
                                    name += "Standard Edition ";
                                }
                            }
                            else if (versionInfo.dwMajorVersion == 5 &&
                                versionInfo.dwMinorVersion == 0)
                            {
                                if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_DATACENTER) == VER_SUITE_DATACENTER)
                                {
                                    name += "Datacenter Server ";
                                }
                                else if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_ENTERPRISE) == VER_SUITE_ENTERPRISE)
                                {
                                    name += "Advanced Server ";
                                }
                                else
                                {
                                    name += "Server ";
                                }
                            }
                            // Windows NT 4.0 
                            else
                            {
                                if ((((OSVERSIONINFOEX)versionInfo).wSuiteMask & VER_SUITE_ENTERPRISE) == VER_SUITE_ENTERPRISE)
                                {
                                    name += "Server 4.0, Enterprise Edition ";
                                }
                                else
                                {
                                    name += "Server 4.0 ";
                                }
                            }
                        }
                    }
                    break;

                // Win 9X家族 
                case VER_PLATFORM_WIN32_WINDOWS:
                    if (versionInfo.dwMajorVersion == 4 && versionInfo.dwMinorVersion == 0)
                    {
                        name = "Microsoft Windows 95 ";
                        if (versionInfo.szCSDVersion[1] == 'C' ||
                            versionInfo.szCSDVersion[1] == 'B')
                        {
                            name += "OSR2 ";
                        }
                    }

                    if (versionInfo.dwMajorVersion == 4 && versionInfo.dwMinorVersion == 10)
                    {
                        name = "Microsoft Windows 98 ";
                        if (versionInfo.szCSDVersion[1] == 'A')
                        {
                            name = "SE ";
                        }
                    }

                    if (versionInfo.dwMajorVersion == 4 && versionInfo.dwMinorVersion == 90)
                    {
                        name = "Microsoft Windows Millennium Edition";
                    }
                    break;

                // 其他Win32系统 
                case VER_PLATFORM_WIN32s:
                    name = "Microsoft Win32s";
                    break;

                default:
                    name = "Unknown System";
                    break;
            }

            name += versionInfo.szCSDVersion;

            return name;
        }
    }
}
