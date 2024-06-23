﻿using System;
using NextLoad.Models;

namespace NextLoad.Controllers
{
    public class LogController
    {
        public static void CreateLog(string user, string item, string id_item, string action, string campos)
        {
            using (NextLoadEntities context = new NextLoadEntities())
            {
                Log log = new Log();

                log.fecha = DateTime.Now;
                log.usuario = user;
                log.item = item;
                log.id_item = id_item;
                log.accion = action;
                log.campos = campos;

                context.Log.Add(log);
                context.SaveChanges();
            }
        }
    }
}