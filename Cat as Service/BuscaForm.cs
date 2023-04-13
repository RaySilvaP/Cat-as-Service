using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cat_as_Service.Models;

namespace Cat_as_Service
{
    public partial class BuscaForm : Form
    {
        private Breed[] breeds;
        string selected;
        string imageId;

        public BuscaForm()
        {
            InitializeComponent();

            if (breeds == null)
            {
                breeds = Service.inst.GetBreeds();
                cbBreeds.Items.AddRange(breeds.Select(b => b.name).ToArray());
            }
            else
            {
                cbBreeds.Items.AddRange(breeds.Select(b => b.name).ToArray());
            }
        }

        private void cbBreeds_SelectedIndexChanged(object sender, EventArgs e)
        {
            selected = cbBreeds.SelectedItem.ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                Breed breed = Array.Find(breeds, b => b.name.Equals(selected));
                tbTemperament.Text = breed.temperament;
                tbOrigin.Text = breed.origin;
                tbDescription.Text = breed.description;

                imageId = Service.inst.GetImageId(breed.id);
            }
        }

        private void btnFavoritar_Click(object sender, EventArgs e)
        {
            if(selected != null)
            {
                Breed breed = Array.Find(breeds, b => b.name.Equals(selected));
                string breedId = breed.id;
                string error = Service.inst.PostFavorite(imageId, breedId);

                if (error != null)
                {
                    MessageBox.Show(error);
                }
                else
                {
                    MessageBox.Show($"{breed.name} foi favoritado!");
                }
            }
        }
    }
}
