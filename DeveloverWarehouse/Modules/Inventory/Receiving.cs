﻿using Develover.GUI;
using Develover.GUI.Forms;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeveloverWarehouse.Modules.Inventory
{
    public partial class Receiving : DeveloverForm, IDeveloverFormChild
    {
        public Receiving()
        {
            InitializeComponent();
        }

        private async void But_item_new_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await ((IDeveloverFormParent)this.MdiParent).SetStatusAsync("vừa new xong");

                for (int i = 0; i < 1000; i++)
                    await ((IDeveloverFormParent)this.MdiParent).SetStatusAsync("đang làm " + i);


            await ((IDeveloverFormParent)this.MdiParent).SetStatusAsync("Ready");
        }

        private void But_item_cancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            ((IDeveloverFormParent)this.MdiParent).SetStatusAsync("vừa cancel xong");

            Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                    ((IDeveloverFormParent)this.MdiParent).SetStatusAsync("đang làm " + i);
            });


            ((IDeveloverFormParent)this.MdiParent).SetStatusAsync("Ready");
        }
    }
}