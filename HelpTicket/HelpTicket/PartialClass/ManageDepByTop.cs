using Business;
using Business.Implementar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelpTicket.PartialClass
{
    public class ManageDepByTop
    {
        //public List<ListadoDepbyTop> lista { get; set; }
        public List<TopicoSelected> topicos { get; set; }

        [Required(ErrorMessage = " El trabajador es obligatorio")]
        [Display(Name = "trabajador")]
        public string trabajador { get; set; }

        private IDepartamentoService ds = new DepartamentoService();
        private ITopicoService ts = new TopicoService();

        public ManageDepByTop()
        {
            //lista = new List<ListadoDepbyTop>();
            topicos = new List<TopicoSelected>();
        }

        public void llenarDatos()
        {
            //var depatamento = ds.FindAll();
            //foreach(var item in depatamento)
            //{
            //    ListadoDepbyTop obj = new ListadoDepbyTop();
            //    obj.nombreDepartamento = item.departamento;
            //    var topicos = ts.FindByDepartamento(item.id, null);
            //    foreach (var top in topicos)
            //    {
            //        obj.topicos.Add(new TopicoSelected(top.id, top.topico));
            //    }
            //    lista.Add(obj);
            //    topicos.Clear();
            //}
            //depatamento.Clear();
            var top = ts.FindAll();
            foreach (var item in top)
            {
                topicos.Add(new TopicoSelected(item.id, item.topico));
            }
        }
    }
}