﻿using System;
using System.Linq;

namespace EasyConsole
{
    public abstract class Page
    {
        public Page(string title, Program program)
        {
            Title = title;
            Program = program;
        }

        public string Title { get; }

        public Program Program { get; set; }

        public virtual void Display()
        {
            if (Program.History.Count > 1 && Program.BreadcrumbHeader)
            {
                string breadcrumb = null;
                foreach (var title in Program.History.Select(page => page.Title).Reverse())
                {
                    breadcrumb += title + " > ";
                }

                breadcrumb = breadcrumb.Remove(breadcrumb.Length - 3);
                Console.WriteLine(breadcrumb);
            }
            else
            {
                Console.WriteLine(Title);
            }

            Console.WriteLine("---");
        }
    }
}