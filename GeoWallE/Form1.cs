﻿using Compiling;
using Syncfusion.Windows.Forms.Edit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeoWallE
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            editcontrol = GetEditControl();
            editcontrol.Dock = DockStyle.Fill;
            codePanel.Controls.Add(editcontrol);

            canvas1.Clear(System.Drawing.Color.LightSteelBlue);
            //canvas1.SetColor(Color.Blue);
            //canvas1.DrawLine(new Point(140, 320), new Point(200, 200), "");
            //canvas1.SetColor(Color.Black);
           // canvas1.DrawPoint(new Point(140, 320), "");
           // canvas1.DrawPoint(new Point(200, 200), "");
        }

        EditControl editcontrol;

        private EditControl GetEditControl()
        {
            var sceneCodeTextBox = new EditControl();

            sceneCodeTextBox.AcceptsReturn = true;
            sceneCodeTextBox.AcceptsTab = true;
            sceneCodeTextBox.AllowDrop = true;
            sceneCodeTextBox.Font = new System.Drawing.Font("Consolas", 10, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            sceneCodeTextBox.Location = new System.Drawing.Point(8, 0);
            sceneCodeTextBox.Multiline = true;
            sceneCodeTextBox.Name = "tbCode";
            sceneCodeTextBox.Size = new System.Drawing.Size(568, 352);
            sceneCodeTextBox.TabIndex = 0;
            sceneCodeTextBox.Text = "";
            sceneCodeTextBox.WordWrap = false;

            sceneCodeTextBox.StatusBarVisible = false;
            sceneCodeTextBox.LineNumberMarginVisible = true;
            sceneCodeTextBox.IndicatorMarginVisible = false;
            sceneCodeTextBox.OutliningEnabled = true;
            sceneCodeTextBox.BraceMatchingEnabled = true;
            sceneCodeTextBox.WhiteSpaceVisible = false;
            sceneCodeTextBox.KeepTabs = false;
            sceneCodeTextBox.ContextPromptEnabled = false;
            sceneCodeTextBox.ContextChoiceEnabled = false;
            sceneCodeTextBox.SyntaxColoringEnabled = true;
            sceneCodeTextBox.GridLinesVisible = false;
            sceneCodeTextBox.IndentType = EditIndentType.Block;
            sceneCodeTextBox.ShowSplitterButton = false;

            sceneCodeTextBox.AutomaticOutliningEnabled = true;

            sceneCodeTextBox.StartAutomaticOutlining();

            sceneCodeTextBox.AddColorGroup("keywords", System.Drawing.Color.Blue, System.Drawing.Color.White, true, false, EditColorGroupType.RegularText);
            //string[] keywords = "if,then,else,undefined,let,in,intersect,and,or,not,draw,include,color,restore,clear".Split(',');
            foreach (string k in Compiling.Lexical.Keywords)
                sceneCodeTextBox.AddKeyword(k, "keywords");

            sceneCodeTextBox.AddColorGroup("strings", System.Drawing.Color.FromArgb(255, 193, 21, 67), System.Drawing.Color.White, true, false, EditColorGroupType.RegularText);
            sceneCodeTextBox.AddTag("\"", "\"", "\\", false, "strings");

            sceneCodeTextBox.AddColorGroup("comments", System.Drawing.Color.DarkGreen, System.Drawing.Color.White, true, false, EditColorGroupType.RegularText);
            sceneCodeTextBox.AddTag("/*", "*/", "", true, "comments");
            sceneCodeTextBox.AddTag("//", "", "", false, "comments");

            sceneCodeTextBox.Dock = DockStyle.Fill;

            return sceneCodeTextBox;
        }

        private void SaveAll()
        {
            editcontrol.Save();
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAll();

            if (!File.Exists(editcontrol.CurrentFile))
                return;

            var code = File.ReadAllText(editcontrol.CurrentFile);

            List<CompilingError> errors = new List<CompilingError> ();
            var tokens = Compiling.Lexical.GetTokens(editcontrol.CurrentFile, code, errors);

            //Borrar
           

            var parser = new KeyParser(tokens);
            Drawer painter = new Drawer(canvas1);            
            int initial = errors.Count;

            var stat = parser.ParserProgram(errors);
            canvas1.Clear(System.Drawing.Color.LightSteelBlue);
            if (errors.Count == initial) { stat.CheckSemantics(new ScopeCheckSemantic(), errors); }
            if (errors.Count == initial) { stat.Run(new ScopeExecution(errors, painter), painter); }

            
           

            if (errors.Any())
            {
                string errorList = "Error!\n";
                foreach (var er in errors)
                    errorList += string.Format("{0}: {1} at {2} line {3}\n", er.Code, er.Argument, Path.GetFileName(er.Location.File), er.Location.Line);
                output.Text = errorList;
            }
            else
            {
                string o = "Done!\n";
                foreach (var token in tokens)
                    o += token.ToString() + "\n";
                output.Text = o;
            }
        }

       
    }
}
