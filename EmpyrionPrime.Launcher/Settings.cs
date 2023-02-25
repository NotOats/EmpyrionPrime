using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpyrionPrime.Launcher;

internal class Settings
{
    public required EmpyrionSettings Empyrion { get; set; }

}

internal class EmpyrionSettings
{
    public required string ServerPath { get; set; }
    public string DedicatedFile { get; set; } = "dedicated.yaml";

    public string EpmAddress { get; set; } = "127.0.0.1";
    public int EpmPort { get; set; } = 12345;
    public int EpmClientId { get; set; } = -1;
}
