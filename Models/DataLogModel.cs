using FileMonitor.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMonitor.Models
{
  public class DataLogModel
  {
    public string File { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
  }
}
