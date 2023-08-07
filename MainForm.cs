#region Header
// Copyright© 2003-2023 https://github.com/ArdeshirV, Licensed under MIT

using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;

#endregion
//-----------------------------------------------------------------------------
namespace Life
{
	/// <summary>
	/// Startup form.
	/// </summary>
	public class MainForm: System.Windows.Forms.Form
	{
		#region Variables

		private int m_intCounter = 0;
		private int m_intActiveSprite = 1;
		private const int m_intMaxArr = 100;
		private int[,] m_intArr = new int[m_intMaxArr, m_intMaxArr];

        #region designer variable

        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        #endregion

        #endregion
        //---------------------------------------------------------------------
		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="args">Command line parameter</param>
		public MainForm(string[] args)
		{

			InitializeComponent();

			int l_intIndexerI, l_intIndexerJ, l_intIndexer = 1;
			Random l_rnd = new Random((int)DateTime.Now.Second);

			for(l_intIndexerI = 0; l_intIndexerI < m_intMaxArr; l_intIndexerI++)
				for(l_intIndexerJ = 0; l_intIndexerJ < m_intMaxArr; l_intIndexerJ++)
					m_intArr[l_intIndexerI, l_intIndexerJ] = 0;

			//for(l_intIndexer = 1; l_intIndexer < 4; l_intIndexer++)
			//{
				l_intIndexerI = l_rnd.Next(10, 89);
				l_intIndexerJ = l_rnd.Next(10, 89);

				m_intArr[l_intIndexerI, l_intIndexerJ] = l_intIndexer;
				m_intArr[l_intIndexerI - 1, l_intIndexerJ] = l_intIndexer;
				m_intArr[l_intIndexerI + 1, l_intIndexerJ] = l_intIndexer;
				m_intArr[l_intIndexerI, l_intIndexerJ - 1] = l_intIndexer;
				m_intArr[l_intIndexerI, l_intIndexerJ + 1] = l_intIndexer;
				m_intArr[l_intIndexerI - 2, l_intIndexerJ] = l_intIndexer;
				m_intArr[l_intIndexerI + 2, l_intIndexerJ] = l_intIndexer;
				m_intArr[l_intIndexerI, l_intIndexerJ - 2] = l_intIndexer;
				m_intArr[l_intIndexerI, l_intIndexerJ + 2] = l_intIndexer;
				m_intArr[l_intIndexerI - 1, l_intIndexerJ - 1] = l_intIndexer;
				m_intArr[l_intIndexerI + 1, l_intIndexerJ + 1] = l_intIndexer;
			//}

			Visible = false;
			Timer m_timTimer = new Timer();
			m_timTimer.Interval = 100;
			m_timTimer.Enabled = true;
			m_timTimer.Tick += new EventHandler(m_timTimer_Ticker);
		}

		#endregion
		//---------------------------------------------------------------------
		#region Event handlers

		/// <summary>
		/// Occured whenever main form has been painted.
		/// </summary>
		/// <param name="sender">Main form</param>
		/// <param name="e">Event argument</param>
		private void MainForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Bitmap l_bmpOffScreen = new Bitmap(ClientSize.Width, ClientSize.Height, e.Graphics);
			Graphics l_grpOffScreen = Graphics.FromImage(l_bmpOffScreen);

			OnPaint(l_grpOffScreen);

			e.Graphics.DrawImageUnscaled(l_bmpOffScreen, 0, 0);
		}

		/// <summary>
		/// Occured whenever .
		/// </summary>
		/// <param name="sender">Timer</param>
		/// <param name="e">Event argument</param>
		private void m_timTimer_Ticker(object sender, EventArgs e)
		{
			for(int l_intIndexer = 0; l_intIndexer < 9; l_intIndexer++)
				FrameMove(ref m_intArr);

			Invalidate();
		}

		/// <summary>
		/// Occured whenever main form has been resized.
		/// </summary>
		/// <param name="sender">Main form</param>
		/// <param name="e">Event argument</param>
		private void MainForm_Resize(object sender, System.EventArgs e)
		{
			Invalidate();
		}

		#endregion
		//---------------------------------------------------------------------
		#region Overrided functions

		/// <summary>
		/// Occured when background of main form need to redraw.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
				if(components != null)
					components.Dispose();

			base.Dispose(disposing);
		}

		#endregion
		//---------------------------------------------------------------------
		#region Utility functions

		private int m_intMode = 1;
		private bool m_blnFirst = true;

		/// <summary>
		/// Move current frame.
		/// </summary>
		/// <param name="arr">Array of boolean values</param>
		private void FrameMove(ref int[,] arr)
		{
			int l_intSpriteCounter;

			for(int l_intIndexerI = 0; l_intIndexerI < m_intMaxArr; l_intIndexerI++)
			{
				for(int l_intIndexerJ = 0; l_intIndexerJ < m_intMaxArr; l_intIndexerJ++)
				{
					l_intSpriteCounter = 0;

					if(l_intIndexerI < m_intMaxArr - 1 && l_intIndexerJ > 0)
					{
						if(arr[l_intIndexerI + 1, l_intIndexerJ - 1] == m_intActiveSprite)
							l_intSpriteCounter++;
					}

					if(l_intIndexerI > 0 && l_intIndexerJ < m_intMaxArr - 1)
					{
						if(arr[l_intIndexerI - 1, l_intIndexerJ + 1] == m_intActiveSprite)
							l_intSpriteCounter++;
					}

					if(l_intIndexerI < m_intMaxArr - 1 && l_intIndexerJ < m_intMaxArr - 1)
					{
						if(arr[l_intIndexerI + 1, l_intIndexerJ + 1] == m_intActiveSprite)
							l_intSpriteCounter++;
					}

					if(l_intIndexerI > 0)
					{
						if(arr[l_intIndexerI - 1, l_intIndexerJ] == m_intActiveSprite)
							l_intSpriteCounter++;
					}

					if(l_intIndexerI > 0 && l_intIndexerJ > 0)
					{
						if(arr[l_intIndexerI - 1, l_intIndexerJ - 1] == m_intActiveSprite)
							l_intSpriteCounter++;
					}

					if(l_intIndexerJ > 0)
					{
						if(arr[l_intIndexerI, l_intIndexerJ - 1] == m_intActiveSprite)
							l_intSpriteCounter++;
					}

					if(l_intIndexerI < m_intMaxArr - 1)
					{
						if(arr[l_intIndexerI + 1, l_intIndexerJ] == m_intActiveSprite)
							l_intSpriteCounter++;
					}

					if(l_intIndexerJ < m_intMaxArr - 1)
					{
						if(arr[l_intIndexerI, l_intIndexerJ + 1] == m_intActiveSprite)
							l_intSpriteCounter++;
					}

					switch(m_intMode)
					{
						case 1:
							if(l_intSpriteCounter == 3)
								arr[l_intIndexerI, l_intIndexerJ] = m_intActiveSprite;
							break;
						case 2:
							if(l_intSpriteCounter < 2)
								arr[l_intIndexerI, l_intIndexerJ] = 0;
							break;
						case 3:
							if(l_intSpriteCounter > 3)
								arr[l_intIndexerI, l_intIndexerJ] = 0;
							break;
					}
				}
			}

			if(++m_intMode > 3)
				m_intMode = 1;
		}

		/// <summary>
		/// Main drawing method.
		/// </summary>
		/// <param name="g">Off Screen</param>
		private void OnPaint(Graphics g)
		{
			Brush l_brsNew = Brushes.Red;
			float l_fltW = (float)ClientSize.Width / (float)m_intMaxArr,
				  l_fltH = (float)ClientSize.Height / (float)m_intMaxArr;

			switch(m_intCounter++ % 4)
			{
				case 1:
					l_brsNew = Brushes.Blue;
					break;
				case 2:
					l_brsNew = Brushes.Green;
					break;
				case 3:
					l_brsNew = Brushes.Yellow;
					break;
				default:
					m_intActiveSprite = 1;
					l_brsNew = Brushes.Red;
					break;
			}

			if(m_blnFirst)
			{
                // TODO: Text out...
				//Font f = new Font("MS COMIC SANSSERIF", 100);
              	//g.DrawString("Hi I am virus!!", f, Brushes.Firebrick, 200, 200);
                //Font f = new Font("MS COMIC SANSSERIF", 100, FontStyle.Bold);
				//g.DrawString(Copyright(), this.Font, Brushes.Black, 40, 40);
				m_blnFirst = false;
			}

			for(int l_intIndexerI = 0; l_intIndexerI < m_intMaxArr; l_intIndexerI++)
			{
				for(int l_intIndexerJ = 0; l_intIndexerJ < m_intMaxArr; l_intIndexerJ++)
				{
					if(m_intArr[l_intIndexerI, l_intIndexerJ] > 0)
					{
						g.FillRectangle(l_brsNew, l_intIndexerI * l_fltW,
							l_intIndexerJ * l_fltH, l_fltW, l_fltH);
						g.DrawRectangle(Pens.Gray,
							l_intIndexerI * l_fltW, l_intIndexerJ * l_fltH, l_fltW, l_fltH);
					}
				}
			}
		}

		#endregion
		//---------------------------------------------------------------------
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(412, 339);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Life";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);

		}

		#endregion
		//---------------------------------------------------------------------
		#region Entry point

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				Application.Run(new Life.MainForm(args));
			}
			catch(Exception exp)
			{
				MessageBox.Show(exp.Message, "Error!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		#endregion
	}
}


