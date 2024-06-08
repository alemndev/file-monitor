
using FileMonitor.Models;
using MongoDB.Driver;
using System.IO;
using System;
using FileMonitor.Enums;

namespace FileMonitor.Methods
{
  public class MongoDBMethods
  {
    public void SaveInDatabase(FileSystemEventArgs e, IMongoCollection<DataLogModel> collection)
    {
      try
      {
        collection.InsertOne(new DataLogModel
        {
          File = e.Name,
          Type = GetTypeDataLog(e.ChangeType).ToString(),
        });
      }
      catch (Exception)
      {
        throw;
      }
    }

    public void SaveInDatabase(RenamedEventArgs e, IMongoCollection<DataLogModel> collection)
    {
      try
      {
        collection.InsertOne(new DataLogModel
        {
          File = e.Name,
          Type = GetTypeDataLog(e.ChangeType).ToString(),
        });
      }
      catch (Exception)
      {
        throw;
      }
    }

    private TypeDataLogEnum GetTypeDataLog(WatcherChangeTypes eventType)
    {
      switch (eventType)
      {
        case WatcherChangeTypes.Created:
          return TypeDataLogEnum.Created;
        case WatcherChangeTypes.Deleted:
          return TypeDataLogEnum.Deleted;
        case WatcherChangeTypes.Renamed:
          return TypeDataLogEnum.Renamed;
        case WatcherChangeTypes.Changed:
          return TypeDataLogEnum.Changed;
        default:
          throw new ArgumentException("Unsopported type.");
      }
    }
  }
}
