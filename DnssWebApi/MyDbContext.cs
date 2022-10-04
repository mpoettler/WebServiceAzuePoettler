/**
 * Web Service Development -  FH Joanneum SS22
 * Project: Rest Azure
 * @author  Matthias Pöttler
 * @version 1.0
 * @date   4/10/2022
**/
using Microsoft.EntityFrameworkCore;

namespace DnssWebApi
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
    }
}