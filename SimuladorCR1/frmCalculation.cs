using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimuladorCR1
{
    public partial class frmCalculation : Form
    {
        public frmCalculation()
        {
            InitializeComponent();
            defaultSettingsSpinners(seImpuestoConsumo);
        }

        private void defaultSettingsSpinners(SpinEdit sp)
        {
            sp.Properties.EditFormat.FormatString = "n4";
            sp.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            seCaja.EditValue = 0;
            seElaboracionDA.EditValue = 0;
            seFCA.EditValue = 0;
            seFCAUSD.EditValue = 0;
            seFlete.EditValue = 0;
            seImpuestoConsumo.EditValue = 0;
            seManejoDocumento.EditValue = 0;
            seMargenCadena.EditValue = 0;
            seMargenDistribuidor.EditValue = 0;
            seParqueo.EditValue = 0;
            sePieza.EditValue = 0;
            sePrecioFactura.EditValue = 0;
            seRevisionFrontera.EditValue = 0;
            seRevisionSAT.EditValue = 0;
            seSeguroSobrePrecio.EditValue = 0;
            seSemaforo.EditValue = 0;
            seTipoCambio.EditValue = 0;
            seUnidadesPorCaja.EditValue = 0;
            seVolumenPorUnidad.EditValue = 0;
        }

        private void btnMostrarResultado_Click(object sender, EventArgs e)
        {
            CalcularPrecioFactura();
            CalcularSeguroSobrePrecio();
            CalcularFCA();
            CalcularFCAUSD();
        }

        private void CalcularFCAUSD()
        {
            try
            {
                seFCAUSD.EditValue = double.Parse(seFCA.EditValue.ToString()) / double.Parse(seTipoCambio.EditValue.ToString());
            }
            catch (Exception ex)
            {
                seFCAUSD.EditValue = 0;
            }
        }

        private void CalcularFCA()
        {
            try
            {
                seFCA.EditValue = double.Parse(sePrecioFactura.EditValue.ToString()) - double.Parse(seSeguroSobrePrecio.EditValue.ToString());
            }
            catch (Exception ex)
            {
                seFCA.EditValue = 0;
            }
        }

        private void CalcularSeguroSobrePrecio()
        {
            try
            {
                seSeguroSobrePrecio.EditValue = double.Parse(sePrecioFactura.EditValue.ToString()) * 0.0045;
            }
            catch (Exception ex)
            {
                seSeguroSobrePrecio.EditValue = 0;
            }
        }

        private void CalcularPrecioFactura()
        {
            try
            {
                sePrecioFactura.EditValue = double.Parse(seMargenDistribuidor.EditValue.ToString()) - double.Parse(seCostInternacion.EditValue.ToString());
            }
            catch (Exception ex)
            {
                sePrecioFactura.EditValue = 0;
            }
        }

        private void seUnidadesPorCaja_EditValueChanged(object sender, EventArgs e)
        {
            calcularCaja();
        }

        private void sePieza_EditValueChanged(object sender, EventArgs e)
        {
            calcularCaja();
        }

        private void calcularCaja()
        {
            try
            {
                seCaja.EditValue = double.Parse(seUnidadesPorCaja.EditValue.ToString()) * double.Parse(sePieza.EditValue.ToString());
            } catch (Exception ex)
            {
                seCaja.EditValue = 0;
            }
            
        }

        private void seCaja_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                seImpuestoConsumo.EditValue = double.Parse(seCaja.EditValue.ToString()) / (1 + 0.13);
            }
            catch (Exception ex)
            {
                seImpuestoConsumo.EditValue = 0;
            }
        }

        private void seImpuestoConsumo_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                seMargenCadena.EditValue = double.Parse(seImpuestoConsumo.EditValue.ToString()) * (1 - 0.30);
            }
            catch (Exception ex)
            {
                seMargenCadena.EditValue = 0;
            }
        }

        private void seMargenCadena_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                seMargenDistribuidor.EditValue = double.Parse(seMargenCadena.EditValue.ToString()) * (1 - 0.20);
            }
            catch (Exception ex)
            {
                seMargenDistribuidor.EditValue = 0;
            }
        }

        private void seElaboracionDA_EditValueChanged(object sender, EventArgs e)
        {
            CalcularCostoInternacion();
        }
        
        private void CalcularCostoInternacion()
        {
            try
            {
                seCostInternacion.EditValue = double.Parse(seElaboracionDA.EditValue.ToString()) +
                                                double.Parse(seRevisionFrontera.EditValue.ToString()) +
                                                double.Parse(seManejoDocumento.EditValue.ToString()) +
                                                double.Parse(seRevisionSAT.EditValue.ToString()) +
                                                double.Parse(seSemaforo.EditValue.ToString()) +
                                                double.Parse(seParqueo.EditValue.ToString()) +
                                                double.Parse(seFlete.EditValue.ToString());
            }
            catch (Exception ex)
            {
                seCostInternacion.EditValue = 0;
            }
        }

        private void seRevisionFrontera_EditValueChanged(object sender, EventArgs e)
        {
            CalcularCostoInternacion();
        }

        private void seManejoDocumento_EditValueChanged(object sender, EventArgs e)
        {
            CalcularCostoInternacion();
        }

        private void seRevisionSAT_EditValueChanged(object sender, EventArgs e)
        {
            CalcularCostoInternacion();
        }

        private void seSemaforo_EditValueChanged(object sender, EventArgs e)
        {
            CalcularCostoInternacion();
        }

        private void seParqueo_EditValueChanged(object sender, EventArgs e)
        {
            CalcularCostoInternacion();
        }

        private void seFlete_EditValueChanged(object sender, EventArgs e)
        {
            CalcularCostoInternacion();
        }
    }
}
