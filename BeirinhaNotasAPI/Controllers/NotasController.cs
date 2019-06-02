using BeirinhaNotasAPI.Models;
using BeirinhaNotasAPI.Servicos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;

namespace BeirinhaNotasAPI.Controllers
{
    public class NotasController : Controller
    {
        HttpClient client = new HttpClient();
                
        public NotasController()
        {
            client.BaseAddress = new Uri("http://www.deveup.com.br/notas/api/notes");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        // GET: Notes
        public ActionResult Index()
        {
            List<Nota> notas = new List<Nota>();

            HttpResponseMessage response = client.GetAsync("/api/notes").Result;
            if (response.IsSuccessStatusCode)
            {
                notas = response.Content.ReadAsAsync<List<Nota>>().Result;
            }

            return View(notas);
        }

        // GET: Notes/Details/5
        public ActionResult Details(int id)
        {
            HttpResponseMessage response = client.GetAsync($"/api/notes/{id}").Result;
            Nota note = response.Content.ReadAsAsync<Nota>().Result;
            if (note != null)
                return View(note);
            else
                return NotFound();
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        [HttpPost]
        public ActionResult Create(Nota note)
        {
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync<Nota>("/api/notes", note).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Error while creating note.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = client.GetAsync($"/api/notes/{id}").Result;
            Nota note = response.Content.ReadAsAsync<Nota>().Result;

            if (note != null)
                return View(note);
            else
                return NotFound();
        }

        // POST: Notes/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Nota note)
        {
            try
            {
                HttpResponseMessage response = client.PutAsJsonAsync<Nota>($"/api/notes/{id}", note).Result;

                return RedirectToAction("Index");

                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "Error while editing note.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.GetAsync($"/api/notes/{id}").Result;
            Nota nota = response.Content.ReadAsAsync<Nota>().Result;

            if (nota != null)
                return View(nota);
            else
                return NotFound();

        }

        // POST: Notes/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync($"/api/notes/{id}").Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    return RedirectToAction("Index");
                else
                {
                    ViewBag.Error = "Erro ao deletar";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Error()
        {
            return BadRequest();
        }
    }
}