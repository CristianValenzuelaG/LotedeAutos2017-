﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using LoteAutos2017.Comun;


//http://www.aforgenet.com/framework/downloads.html
namespace LoteAutos2017.UserControls
{
    public partial class ucWebCam : UserControl
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        public String ImagenString { get; set; }
        public Bitmap ImagenBitmap { get; set; }

      
       

        public ucWebCam()
        {
            InitializeComponent();

        }

        private void ucWebCam_Load(object sender, EventArgs e)
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            
            foreach (FilterInfo device in videoDevices) {
                cmbCamaras.Items.Add(device.Name);
            }
            if (cmbCamaras.Items.Count > 0)
            {
                cmbCamaras.SelectedIndex = 0;
                videoSource = new VideoCaptureDevice();                
            }
            else {
                lblError.Visible = true;
                btnTomarFoto.Enabled = false;
            }
        }

        private void btnTomarFoto_Click(object sender, EventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                //this.picImagen.Image = null;
                this.picImagen.Image = ImagenBitmap;
                picImagen.Invalidate();
            }
            else {
                videoSource = new VideoCaptureDevice(videoDevices[cmbCamaras.SelectedIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(videoSource_newFrame);
                videoSource.Start();
            }
        }

        private void videoSource_newFrame(object sender, NewFrameEventArgs eventArgs)
        {
            ImagenBitmap = (Bitmap)eventArgs.Frame.Clone();
            ImagenString = ToolImagen.ToBase64String(ImagenBitmap, ImageFormat.Jpeg);
            picImagen.Image = ImagenBitmap;            
        }

        public void FinalizarControles() {
            if (videoSource.IsRunning) {
                videoSource.Stop();
            }
        }
        public void PonerFotografia(String pathImagen) {
            ImagenBitmap = new System.Drawing.Bitmap(pathImagen);
            ImagenString = ToolImagen.ToBase64String(ImagenBitmap, ImageFormat.Jpeg);
            picImagen.Image = ImagenBitmap;
        }

    }

}
