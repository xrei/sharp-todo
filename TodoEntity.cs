using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class TODOEntity {
  public int ID;
  public string Status;
  public string Name;
  public string Description;
  private const char sep = '|';

  public TODOEntity(int id, string name, string status, string desc) {
    ID = id;
    Name = name;
    Status = status;
    Description = desc;
  }

  public override string ToString() {
    return ID.ToString() + sep + Name + sep + Status + sep + Description;
  }

  public static TODOEntity FromString(string str) {
    string[] vs = str.Split(sep);
    return new TODOEntity(int.Parse(vs[0]), vs[1], vs[2], vs[3]);
  }
}
