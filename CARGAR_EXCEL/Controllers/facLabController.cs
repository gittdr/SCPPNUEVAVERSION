using CARGAR_EXCEL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CARGAR_EXCEL.Controllers
{
    public class facLabController : Controller
    {
        // GET: FacCp
        public ModelosFact modelFact = new ModelosFact();

        public DataTable facturas()
        {
            return this.modelFact.getFacturas();
        }
        public DataTable tipoCambio()
        {
            return this.modelFact.tipoCambio();
        }
        public DataTable getCartasPorte(string factura)
        {
            return this.modelFact.getCartasPorte(factura);
        }
        public DataTable getTipoCambio(string fecha)
        {
            return this.modelFact.getTipoCambio(fecha);
        }

        public DataTable facturasClientes()
        {
            return this.modelFact.getFacturasClientes();
        }
        public DataTable FacturasPorProcesar(string billto)
        {
            return this.modelFact.getFacturasPorProcesar(billto);
        }
        public DataTable facturasEnviadas()
        {
            return this.modelFact.getFacturasEnviadas();
        }
        public DataTable facturasListadop()
        {
            return this.modelFact.getFacturasListadop();
        }
        //public DataTable facturasListado()
        //{
        //    return this.modelFact.getFacturasListado();
        //}

        public DataTable detalleFacturas(string fact)
        {
            return this.modelFact.getDatosFacturas(fact);
        }
        public DataTable getDatosCPAGDOCTRL(string identificador, string foliocpag)
        {
            return this.modelFact.getDatosCPAGDOCTRL(identificador, foliocpag);
        }

        public DataTable getDatosCPAGDOC(string identificador)
        {
            return this.modelFact.getDatosCPAGDOC(identificador);
        }
        public DataTable getDatosInvoice(string identificador)
        {
            return this.modelFact.getDatosInvoice(identificador);
        }

        public DataTable getDatosMaster(string identificador)
        {
            return this.modelFact.getDatosMaster(identificador);
        }

        public void insertaFactura(string fact, string fecha)
        {
            this.modelFact.insertaFactura(fact, fecha);
        }
        public DataTable getDatosSegmentos(string orden)
        {
            return this.modelFact.getDatosSegmentos(orden);
        }
    }
}
