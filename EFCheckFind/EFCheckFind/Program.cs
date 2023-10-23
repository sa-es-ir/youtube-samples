using EFCheckFind;
using Microsoft.EntityFrameworkCore;

var context = new ApplicationDbContext();

var entity = context.Authors.Find(1);

var entity2 = context.Authors.Find(1);

Console.ReadLine();