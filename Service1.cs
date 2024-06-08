using FileMonitor.Models;
using MongoDB.Driver;
using System.IO;
using System.ServiceProcess;
using System.Configuration;
using System;
using FileMonitor.Enums;
using FileMonitor.Methods;

namespace FileMonitor
{
  public partial class FileMonitor : ServiceBase
  {
    private FileSystemWatcher _watcher;
    private MongoClient _client;
    private IMongoDatabase _database;
    private IMongoCollection<DataLogModel> _collection;
    private MongoDBMethods _mongoDBMethods = new MongoDBMethods();

    public FileMonitor()
    {
      InitializeComponent();
      InitWatcher();
      InitDatabase();
    }

    protected override void OnStart(string[] args)
    {
      _watcher.EnableRaisingEvents = true;
    }

    protected override void OnStop()
    {
      _watcher.EnableRaisingEvents = false;
    }

    private void InitWatcher()
    {
      try
      {
        _watcher = new FileSystemWatcher(ConfigurationManager.AppSettings["route"]);
        _watcher.Created += WatcherEvent;
        _watcher.Renamed += WatcherEvent;
        _watcher.Changed += WatcherEvent;
        _watcher.Deleted += WatcherEvent;
      }
      catch (Exception)
      {
        throw;
      }
    }

    private void InitDatabase()
    {
      try
      {
        _client = new MongoClient(ConfigurationManager.AppSettings["mongodb_uri"]);
        _database = _client.GetDatabase(ConfigurationManager.AppSettings["mongodb_database"]);
        _collection = _database.GetCollection<DataLogModel>(ConfigurationManager.AppSettings["mongodb_collection"]);
      }
      catch (Exception)
      {
        throw;
      }
    }

    private void WatcherEvent(object sender, FileSystemEventArgs e)
    {
      _mongoDBMethods.SaveInDatabase(e, _collection);
    }

    private void WatcherEvent(object sender, RenamedEventArgs e)
    {
      _mongoDBMethods.SaveInDatabase(e, _collection);
    }
  }
}
