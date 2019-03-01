using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using NHibernate;
using TarefasDiarias.Models;

namespace TarefasDiarias.Controllers {
    // Implementa os métodos CRUD
    public class HomeController : Controller {
        // GET: Todas as tarefas registradas
        public ActionResult Index() {
            ViewBag.Message = "Lista de tarefas diária.";
            IList<Tarefa> tarefas;
            
            using (ISession session = NHibernateHelper.OpenSession()) {
                tarefas = session.Query<Tarefa>().ToList();
            }

            return View(tarefas);
        }

        // GET: Create
        public ActionResult Create() {
            return View();
        }

        // POST: Create
        [HttpPost]
        public ActionResult Create(FormCollection collection) {
            try {
                Tarefa newtodo = new Tarefa {
                    Titulo = collection["Titulo"],
                    Detalhes = collection["Detalhes"],
                    Concluida = false,
                    DataExecucao = collection["DataExecucao"].AsDateTime(),
                    HorarioExecucao = collection["HorarioExecucao"].AsDateTime(),
                    DataCriacao = DateTime.Now
                };

                // Creating timestamp object to store in the database
                var string_result = newtodo.DataExecucao.ToString("dd/MM/yyyy") + " " + newtodo.HorarioExecucao.ToString("HH:mm") + ":00";
                DateTime timestamp_result = DateTime.ParseExact(string_result, "dd/MM/yyyy HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
                newtodo.DataExecucao = timestamp_result;

                // Store in the database
                using (ISession session = NHibernateHelper.OpenSession()) {
                    using (ITransaction transaction = session.BeginTransaction()) {
                        session.Save(newtodo);
                        transaction.Commit();
                    }
                }

                // Returns to the main view
                return RedirectToAction("Index");
            } catch (Exception e) {
                return View();
            }
        }

        // GET: Edit/id
        public ActionResult Edit(string id) {
            Tarefa todotask = new Tarefa();

            using (ISession session = NHibernateHelper.OpenSession()) {
                todotask = session.Query<Tarefa>().FirstOrDefault(b => b.Id == id);
            }

            ViewBag.SubmitAction = "Save";
            return View(todotask);
        }

        // POST: Edit/id
        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection) {
            try {
                Tarefa todotask = new Tarefa {
                    Id = id,
                    Titulo = collection["Titulo"],
                    Detalhes = collection["Detalhes"],
                    DataCriacao = DateTime.Now,
                    DataExecucao = collection["DataExecucao"].AsDateTime(),
                    HorarioExecucao = collection["HorarioExecucao"].AsDateTime(),
                    Concluida = Convert.ToBoolean(collection["Concluida"].Split(',')[0])
                };

                var result = todotask.DataExecucao.ToString("dd/MM/yyyy") + " " + todotask.HorarioExecucao.ToString("HH:mm") + ":00";
                DateTime teste = DateTime.ParseExact(result, "dd/MM/yyyy HH:mm:ss",
                    System.Globalization.CultureInfo.InvariantCulture);
                todotask.DataExecucao = teste;

                using (ISession session = NHibernateHelper.OpenSession()) {
                    using (ITransaction transaction = session.BeginTransaction()) {
                        session.SaveOrUpdate(todotask);
                        transaction.Commit();
                    }
                }

                System.Diagnostics.Debug.WriteLine(teste);
                System.Diagnostics.Debug.WriteLine(collection["Concluida"]);

                return RedirectToAction("Index");
            } catch {
                return View();
            }
        }

        // POST && GET: Delete/id
        public ActionResult Delete(string id) {
            using (ISession session = NHibernateHelper.OpenSession()) {
                var todotask = session.Query<Tarefa>().FirstOrDefault(b => b.Id == id);

                using (ITransaction trans = session.BeginTransaction()) {
                    session.Delete(todotask);
                    trans.Commit();
                }
            }

            return RedirectToAction("Index");
        }
    }
}