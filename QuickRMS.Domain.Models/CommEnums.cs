using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRMS.Domain.Models
{

    public enum ControlMode
    {
        网口 = 0,
        串口 = 1
    };
    public enum WorkMode
    {
        手动控制 = 0, 周六日及工作日 = 1, 周日及工作日 = 2, 全部工作日 = 3, 假日 = 4
    };
    public enum ValveControlChannel
    {
        未知=0,
        A通路 = 1,
        B通路 = 2,
        A和B通路 = 3
    };
    public enum WirelessState
    {
        正常 = 0,
        无线 = 85
    }
    public enum WorkStatus
    {
        假日模式 = 0, 工作模式 = 1
    }
    public enum WaterNetStatus
    {
        欠供 = 0, 正常 = 1, 超供 = 2
    }

    public enum HistoryType
    {
        读取正常记录 = 0, 读取参数修改记录 = 1, 模式曲线修改记录 = 2, 温度曲线修改记录 = 3
    }

    public enum TimeSpanType
    {
        假日 = 0, 周六 = 6, 周日 = 7, 工作日 = 1
    }

    public enum SysSetting
    {
        ReadDataInterval = 0,
        BindPort = 1,
        ControlMode = 2
    }


    public enum CheckValveState
    {
        无校阀 = 0,
        一号校阀 = 1,
        二号校阀 = 2
    }

    public enum SettingType
    {
        整体参数 = 0x01,
        曲线起始参数 = 0x02,
        曲线终止参数 = 0x1F,
        工作模式曲线设置 = 0x20,
        周六模式曲线设置 = 0x21,
        周日模式曲线设置 = 0x22,
        假日模式曲线设置 = 0x23

    }
    public enum WorkBy
    {
        曲线 = 0,
        时间 = 1
    }
    public enum ARMControl
    {
        手动控阀 = 0x01,
        修改时间 = 0x02,
        校阀 = 0x03
    }

    public enum HistoryNormalType
    {
        正常历史记录 = 0x0,
        上电记录 = 0xfd,
        断电记录 = 0xfe
    }

    public enum RoleInfo
    {
        监视员 = 0,
        一般用户 = 1,
        高级用户 = 2
    }
}
