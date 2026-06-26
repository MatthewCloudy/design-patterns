using System;
using System.Collections.Generic;
using System.Text;

namespace Adapter
{
    public static class SearchDataLab05
    {
        public static void Search(List<ITarget> l)
        {
            Console.WriteLine("Productions with authors born after 1970:");
            foreach(var el in l)
            {
                if(el.list == null)
                {
                    if (el.authorYear > 1970)
                    {
                        Console.WriteLine($"Movie -> Title: {el.title}, genre: {el.genre}, author: {el.author}, release year: {el.releaseYear}");
                    }
                }
                else
                {
                    Console.WriteLine($"Series-> Title: {el.title}, genre: {el.genre}, author: {el.author}, episodes:");
                    foreach(var v in el.list)
                    {
                        if (v.authorYear > 1970)
                        {
                            Console.WriteLine($"Title: {v.title}, author: {v.author}, release year: {v.releaseYear}");
                        }
                    }
                }

            }
        }
    }
}
