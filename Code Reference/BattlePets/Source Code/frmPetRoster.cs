﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoldTeamRules
{
    public partial class frmPetRoster : Form
    {
        private List<Pet> currentList;

        public frmPetRoster()
        {
            InitializeComponent();
        }

        private void frmPet_Load(object sender, EventArgs e)
        {
            currentList = new List<Pet>();
            petJournal.Focus();
            for (int i = 0; i < frmMain.petList.Count; i++)
            {
                if (frmMain.petList[i].RosterSlot == -1)
                {
                    currentList.Add(frmMain.petList[i]);
                }
            }
            for(int i = 0; i < currentList.Count; i++)
            {
                for(int j = i + 1; j < currentList.Count; j++)
                {
                    if(currentList[i].CurrentHP > currentList[j].CurrentHP)
                    {
                        Pet p = currentList[i];
                        currentList[i] = currentList[j];
                        currentList[j] = p;
                    }
                }
            }
            for(int i = 0; i < currentList.Count; i++)
            {
                for(int j = i + 1; j < currentList.Count; j++)
                {
                    if (currentList[j].RosterSlot == -1)
                    {
                        currentList[j].RosterSlot = 999;
                    }
                    if (currentList[i].RosterSlot == -1)
                    {
                        currentList[i].RosterSlot = 999;
                    }
                    if (currentList[j].RosterSlot < currentList[i].RosterSlot)
                    {
                        Pet temp = currentList[i];
                        currentList[i] = currentList[j];
                        currentList[j] = temp;
                    }
                }
            }
            for(int i = 0; i < currentList.Count; i++)
            {
                 petJournal.Items.Add(frmMain.petList[i].ToString());
            }

            petJournal.SelectedIndex = 0;
            UpdateControls();
        }

        private void UpdateControls()
        {
            pb1.BackgroundImage = currentList[petJournal.SelectedIndex].DisplayImage;
            for (int i = 0; i < 6; i++)
            {
                int z = i + 1;
                foreach (Label l in this.Controls.OfType<Label>())
                {
                    if (l.Name.Equals("lblSlot" + z))
                    {
                        l.Text = currentList[petJournal.SelectedIndex].abilities[i].Name;
                    }
                }
            }
            foreach (TextBox tb in this.Controls.OfType<TextBox>())
            {
                if (tb.Name.Equals("tbtLevel"))
                {
                    tb.Text = currentList[petJournal.SelectedIndex].Level.ToString();
                }
                else if(tb.Name.Equals("tbtHP"))
                {
                    tb.Text = currentList[petJournal.SelectedIndex].CurrentHP.ToString() + "/" + currentList[petJournal.SelectedIndex].HP.ToString();
                }
                else if (tb.Name.Equals("tbtRarity"))
                {
                    tb.Text =currentList[petJournal.SelectedIndex].Rarity;
                }
                else if (tb.Name.Equals("tbtSpeed"))
                {
                    tb.Text = currentList[petJournal.SelectedIndex].Speed.ToString();
                }
                else if (tb.Name.Equals("tbtAtk"))
                {
                    tb.Text = currentList[petJournal.SelectedIndex].Attack.ToString();
                }
                else if (tb.Name.Equals("tbtID"))
                {
                    tb.Text = currentList[petJournal.SelectedIndex].PetID.ToString();
                }
            }
            int cap = 0;
            for(int i = 0; i < currentList[petJournal.SelectedIndex].Level; i++)
            {
                cap += (500 * (i + 1));
            }
            progressBar.Maximum = cap;
            progressBar.Value = currentList[petJournal.SelectedIndex].CurrentXP;
        }

        private void petJournal_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            for (int i = 0; i < currentList.Count; i++)
            {
                if (currentList[i].RosterSlot == 999)
                {
                    currentList[i].RosterSlot = -1;
                }
            }
            this.Close();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            for(int i = 0; i < frmMain.petList.Count; i++)
            {
                if (frmMain.petList[i].PetID.Equals(currentList[petJournal.SelectedIndex].PetID))
                {
                    frmRoster.stay = i;
                }
            }
        }

        private void frmPetRoster_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.W) || e.KeyData.Equals(Keys.Up))
            {
                if (petJournal.SelectedIndex == -1)
                {
                    petJournal.SelectedIndex = -1;
                }
                else
                {
                    petJournal.SelectedIndex--;
                }
            }
            if (e.KeyCode.Equals(Keys.S) || e.KeyData.Equals(Keys.Down))
            {
                if (petJournal.SelectedIndex == currentList.Count - 1)
                {
                    petJournal.SelectedIndex = currentList.Count - 1;
                }
                else
                {
                    petJournal.SelectedIndex++;
                }
            }
        }
    }
}
