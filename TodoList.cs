using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class TODOList {
  public string Path;
  public List<TODOEntity> List = new List<TODOEntity>();
  public TODOList(string path) {
    Path = path;
  }

  public static TODOList Create(string path) {
    if (!File.Exists(path)) {
      File.Create(path).Close();
      return new TODOList(path);
    }
    throw new Exception("File exist");
  }

  public static TODOList Open(string path) {
    if (!File.Exists(path)) {
      throw new Exception("File doesn\'t exist");
    }
    TODOList list = new TODOList(path);
    list.Read();
    return list;
  }

  public void Add(string Name, string Status, string Description) {
    int ID = List.Count == 0 ? 1 : List.Last().ID + 1;
    List.Add(new TODOEntity(ID, Name, Status, Description));
  }
  public void Add(TODOEntity entity) {
    Add(entity.Name, entity.Status, entity.Description);
  }

  public void Read() {
    using (StreamReader sr = new StreamReader(Path)) {
      while (!sr.EndOfStream) {
        List.Add(TODOEntity.FromString(sr.ReadLine()));
      }
    }
  }
  public void Save() {
    using (StreamWriter sw = new StreamWriter(Path, false)) {
      foreach (TODOEntity entity in List) {
        sw.WriteLine(entity.ToString());
      }
    }
  }

  public TODOEntity ReadById(int id) {
    return List.Where((x) => x.ID == id).Single();
  }

  public void Print() {
    Console.WriteLine("ID | Name | Status | Description\r\n");
    foreach (TODOEntity e in List) {
      Console.WriteLine(e);
    }
  }

  public void Remove(TODOEntity e) {
    List.Remove(e);
    Save();
  }

  public void Update(TODOEntity e) {
    TODOEntity u = List.Where(x => x.ID == e.ID).Single();
    u.Name = e.Name;
    u.Status = e.Status;
    u.Description = e.Description;
    Save();
  }
}