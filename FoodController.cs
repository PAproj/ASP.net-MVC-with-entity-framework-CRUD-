using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using _20mvc.Models;
using System.Web.Mvc;
using System.Diagnostics;
using System.Drawing;

namespace _20mvc.Controllers
{
    public class FoodController : Controller
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\aryap\OneDrive\Documents\DBFood.mdf;Integrated Security=True;Connect Timeout=30");
        public ActionResult GetAllFood()
        {
            List<TBLFood> list = new List<TBLFood>();
            SqlCommand cmd = new SqlCommand("select * from TBLFood", conn);
            conn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TBLFood f = new TBLFood();
                f.Id = int.Parse(dr["Id"].ToString());
                f.DishName = dr["DishName"].ToString();
                f.Cuisine = dr["Cuisine"].ToString();
                f.Price = int.Parse(dr["Price"].ToString());
                f.Discount = int.Parse(dr["Discount"].ToString());

                list.Add(f);
            }
            conn.Close();
            return View(list);
        }
        public ActionResult insert()
        {
            return View();
        }

        public ActionResult insertdata(TBLFood t)
        {
            SqlCommand cmd = new SqlCommand("Insert into TBLFood(DishName, Cuisine, Price, Discount) values('" + t.DishName + "', '" + t.Cuisine + "', '" + t.Price.ToString() + "', '" + t.Discount.ToString() + "')", conn);
            conn.Open();
            int x = cmd.ExecuteNonQuery();
            conn.Close();
            if(x > 0)
                return RedirectToAction("GetAllFood");
            else
                return null;
        }
        public ActionResult update (int id)
        {
            TBLFood t = new TBLFood();
            SqlCommand cmd = new SqlCommand("Select * From TBLFood where Id = '" + id + "'", conn);
            conn.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                t.Id = int.Parse(dr[0].ToString());
                t.DishName = dr[1].ToString();
                t.Cuisine = dr[2].ToString();
                t.Price = int.Parse(dr[3].ToString());
                t.Discount = int.Parse(dr[4].ToString());
            }
            conn.Close();
            return View(t);
        }
        [HttpPost]
        public ActionResult updatedata(TBLFood tf)
        {
            SqlCommand cmd = new SqlCommand("update TBLFood set DishName = '" + tf.DishName + "', Cuisine = '" + tf.Cuisine + "', Price = '" + tf.Price.ToString() + "', Discount = '" + tf.Discount.ToString() + "' where Id = '" + tf.Id + "' ", conn);
            conn.Open();
            int a = cmd.ExecuteNonQuery();
            conn.Close();
            if (a > 0)
                return RedirectToAction("GetAllFood");
            else
                return null;
        }
        public ActionResult Delete(int id)
        {
            TBLFood t = new TBLFood();
            SqlCommand cmd = new SqlCommand("Delete From TBLFood where Id = '" + id + "'", conn);
            conn.Open();
            int a = cmd.ExecuteNonQuery();
            conn.Close();
            if (a > 0)
                return RedirectToAction("GetAllFood");
            else
                return null;
        }
    }
}