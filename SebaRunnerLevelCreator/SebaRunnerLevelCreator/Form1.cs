using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



public enum Tools
{
    SELECT,
    MOVE
}

enum GridLines
{
    ONE,
    FIVE,
    TEN
}

namespace SebaRunnerLevelCreator
{

    /// <summary>
    /// @Author: Andrew Seba
    /// @Description: Main Form to edit and create levels for SebaRunner.
    /// Conatains a grid/scene view and rail timelines.
    /// </summary>
    public partial class Form1 : Form
    {
        public List<GameObject> allObjects = new List<GameObject>();
        public float highLightOffset = 1;

        //List<Control> toolCheckBoxes = new List<Control>();

        string levelName = "newLevel";
        string authorName = "myName";

        bool wireframe = false;
        float gridScale = 0.1f;

        GridLines currentGridLines = GridLines.TEN;
        Tools selectedTool = Tools.SELECT;

        //Holds mouse position relative to window.
        Point gridRelative;

        //Holds Position relative to scene scale.
        float SceneX;
        float SceneY;
        //Planning to use these to move the camera around.
        float viewOffsetX = 0;
        float viewOffsetY = 0;
        //Maximum World Values
        decimal maxWorldWidth = 100;
        decimal maxWorldHeight = 100;

        bool snapToGrid = false;
        bool mouseDown = false;
        GameObject selectedObj = null;

        //Loading Variable
        string readline;

        //Time Line Variables
        object selectedTimeNode;
        Point moveTimeRelative;
        Point effectTimeRelative;
        Point facingTimeRelative;

        //Engine Lists
        public List<Movements> movements = new List<Movements>();
        public List<Facings> facings = new List<Facings>();
        public List<Effects> effects = new List<Effects>();

        //Forms
        NodePicker nodePicker;

        public Form1()
        {
            InitializeComponent();
            typeof(Panel).InvokeMember("DoubleBuffered", System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic, null, gridView, new object[] { true });

            hScrollBar1.Maximum = (int)maxWorldWidth;
            vScrollBar1.Maximum = (int)maxWorldHeight;

            #region Didn't work
            ////add all tool checkboxes to toggle on and off easier.
            //foreach (Control box in groupToolsBox.Controls.OfType<Control>())
            //{
            //    if(box.Tag.Equals("toolCheckButton"))
            //    {
            //        toolCheckBoxes.Add(box);
            //    }
            //}
            #endregion

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            nodePicker = new NodePicker();
            nodePicker.form1 = this;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Paint!
        private void gridView_Paint(object sender, PaintEventArgs e)
        {
            #region Zoom Grid Fast
            switch (currentGridLines)
            {
                case GridLines.ONE:
                    for (int i = 0; i <= maxWorldWidth; i++)
                    {
                        for (int k = 0; k <= maxWorldHeight; k++)
                        {
                            e.Graphics.DrawLine(Pens.Gray, (i + viewOffsetX) / gridScale, (k + viewOffsetY) / gridScale, ((i + viewOffsetX + (float)maxWorldWidth)) * gridScale, (k + viewOffsetY) / gridScale);
                            e.Graphics.DrawLine(Pens.Gray, (i + viewOffsetX) / gridScale, (k + viewOffsetY) / gridScale, (i + viewOffsetX) / gridScale, ((k + viewOffsetY + (float)maxWorldHeight)) * gridScale);
                        }
                    }
                    break;
                case GridLines.FIVE:
                    for (int i = 0; i <= maxWorldWidth; i += 5)
                    {
                        for (int k = 0; k < maxWorldHeight; k += 5)
                        {
                            e.Graphics.DrawLine(Pens.Gray, (i + viewOffsetX) / gridScale, (k + viewOffsetY) / gridScale, ((i + viewOffsetX + (float)maxWorldWidth)) * gridScale, (k + viewOffsetY) / gridScale);
                            e.Graphics.DrawLine(Pens.Gray, (i + viewOffsetX) / gridScale, (k + viewOffsetY) / gridScale, (i + viewOffsetX) / gridScale, ((k + viewOffsetY + (float)maxWorldHeight)) * gridScale);
                        }
                    }
                    break;
                case GridLines.TEN:
                    for (int i = 0; i <= maxWorldWidth; i += 10)
                    {
                        for (int k = 0; k <= maxWorldHeight; k += 10)
                        {
                            e.Graphics.DrawLine(Pens.Gray, (i + viewOffsetX) / gridScale, (k + viewOffsetY) / gridScale, ((i + viewOffsetX + (float)maxWorldWidth)) * gridScale, (k + viewOffsetY) / gridScale);
                            e.Graphics.DrawLine(Pens.Gray, (i + viewOffsetX) / gridScale, (k + viewOffsetY) / gridScale, (i + viewOffsetX) / gridScale, ((k + viewOffsetY + (float)maxWorldHeight)) * gridScale);
                        }
                    }
                    break;
                default:
                    break;
            }
            #endregion

            #region slow grid
            //Draw grid
            //for (int i = 0; i < maxWorldWidth; i++)
            //{
            //    for (int k = 0; k < maxWorldHeight; k++)
            //    {
            //        e.Graphics.DrawLine(Pens.Gray, (i + viewOffsetX) / gridScale, (k + viewOffsetY) / gridScale, ((i + viewOffsetX + (float)maxWorldWidth)) / gridScale, (k + viewOffsetY) / gridScale);
            //        e.Graphics.DrawLine(Pens.Gray, (i + viewOffsetX) / gridScale, (k + viewOffsetY) / gridScale, (i + viewOffsetX) / gridScale, ((k + viewOffsetY + (float)maxWorldHeight)) / gridScale);
            //    }
            //}
            #endregion

            //draw objects
            foreach (GameObject obj in allObjects)
            {
                //draw grid



                #region Crate Drawing
                if (obj is Crate)
                {
                    Crate crate = (Crate)obj;

                    e.Graphics.DrawRectangle(Pens.LightYellow,
                        (crate.x - (crate.width / 2) + viewOffsetX) / (gridScale),
                        (crate.z - (crate.height / 2 ) +  viewOffsetY) / gridScale,
                        (crate.width / gridScale),
                        (crate.height / gridScale));
                    if (!wireframe)
                    {
                        e.Graphics.FillRectangle(Brushes.LightYellow,
                            ((crate.x - (crate.width / 2 ) + viewOffsetX) / gridScale + 1 ),
                            ((crate.z - (crate.height / 2) + viewOffsetY) / gridScale + 1),
                            (crate.width / gridScale - 1),
                            (crate.height / gridScale - 1));
                    }
                    //UpdateBounds
                    crate.bounds = new RectangleF(
                        (crate.x - (crate.width / 2) + viewOffsetX) / gridScale,
                        (crate.z - (crate.height / 2) + viewOffsetY) / gridScale,
                        crate.width,
                        crate.height);
                    e.Graphics.DrawLine(Pens.Red, new PointF(crate.bounds.X, crate.bounds.Y), new PointF(crate.bounds.X + crate.bounds.Width / gridScale, crate.bounds.Y + crate.bounds.Height / gridScale));
                }
                else//CircleObjects
                {
                    e.Graphics.DrawEllipse(obj.outlineColor,
                        (obj.x - (obj.width / 2) + viewOffsetX) / gridScale,
                        (obj.z - (obj.height / 2) + viewOffsetY) / gridScale,
                        (obj.width / gridScale),
                        (obj.height / gridScale));

                    if (!wireframe)
                    {
                        e.Graphics.FillEllipse(obj.fillColor,
                            (((obj.x - (obj.width / 2) + viewOffsetX) / gridScale) + 1),
                            (((obj.z - (obj.height / 2) + viewOffsetY) / gridScale) + 1),
                            (obj.width / gridScale - 1),
                            (obj.height / gridScale - 1));
                    }

                    obj.bounds = new RectangleF(
                        (obj.x - (obj.width / 2) + viewOffsetX) / gridScale,
                        (obj.z - (obj.height / 2) + viewOffsetY) / gridScale,
                        (obj.width),
                        (obj.height));

                    //Draw activation range
                    //if (checkBoxAllActivationRange.Checked)
                    //{
                    //    if (obj is Enemy)
                    //    {
                    //        e.Graphics.DrawEllipse(obj.outlineColor, )
                    //    }
                    //}

                }
                #endregion

                #region Waypoint Drawing
                if (obj is Node)
                {
                    Node node = (Node)obj;


                    if (node.isPlayer)
                    {

                        if(movements.Count != 0 && movements[0].endWaypoint != null)
                        {
                            e.Graphics.DrawLine(Pens.White,
                                (node.x + viewOffsetX) / gridScale,
                                (node.z + viewOffsetY) / gridScale,
                                ((movements[0].endWaypoint.x) + viewOffsetX) / gridScale,
                                ((movements[0].endWaypoint.z) + viewOffsetY) / gridScale);
                        }

                    }


                    //draw the movement lines (gizmo lines)
                    if (movements.Count > 1)
                    {
                        for (int i = 1; i < movements.Count; i++)
                        {
                            if (movements[i] != null && movements[i].endWaypoint != null)
                            {
                                
                                GameObject start = movements[i - 1].endWaypoint;
                                GameObject end = movements[i].endWaypoint;

                                if(start != null && end != null)
                                {
                                    //Draw Curve
                                    if(movements[i].moveType == MovementTypes.BEZIER && movements[i].curveWaypoint != null)
                                    {
                                        //C# Graphics.DrawBezier is alot different from the one we made in our project
                                        //e.Graphics.DrawBezier(Pens.White,
                                        //    (start.x + viewOffsetX) / gridScale, (start.z + viewOffsetY) / gridScale,
                                        //    (movements[i].curveWaypoint.x + viewOffsetX) / gridScale, (movements[i].curveWaypoint.z + viewOffsetY) / gridScale,
                                        //    (end.x + viewOffsetX) / gridScale, (end.z + viewOffsetY) / gridScale);
                                        
                                        for(int j = 1; j <= 10; j++)
                                        {
                                            GameObject lineEnd = GetPoint(
                                                new GameObject(start.x, start.y, start.z),
                                                new GameObject(end.x, end.y, end.z),
                                                new GameObject(
                                                    movements[i].curveWaypoint.x,
                                                    movements[i].curveWaypoint.y,
                                                    movements[i].curveWaypoint.z),
                                                j / 10f);

                                            e.Graphics.DrawLine(Pens.White,
                                                (start.x + viewOffsetX) / gridScale, (start.z + viewOffsetY) / gridScale,
                                                (lineEnd.x + viewOffsetX) / gridScale, (lineEnd.z + viewOffsetY) / gridScale);

                                            start = lineEnd;
                                        }
                                        
                                    }
                                    else
                                    {
                                        //Highlight line if selected
                                        if (selectedObj == end)
                                        {
                                            e.Graphics.DrawLine(Pens.Yellow,
                                                (start.x + viewOffsetX) / gridScale, (start.z + viewOffsetY) / gridScale,
                                                (end.x + viewOffsetX) / gridScale, (end.z + viewOffsetY) / gridScale);
                                        }
                                        //Draw Normal Line
                                        else
                                        {
                                            e.Graphics.DrawLine(Pens.White,
                                                (start.x + viewOffsetX) / gridScale, (start.z + viewOffsetY) / gridScale,
                                                (end.x + viewOffsetX) / gridScale, (end.z + viewOffsetY) / gridScale);
                                        }
                                    }

                                }
                            }
                        }
                    }

                }
                #endregion
                #region selection circle
                if (obj == selectedObj)
                {
                    e.Graphics.DrawEllipse(
                        Pens.Yellow,
                        ((obj.x - (obj.width / 2) + viewOffsetX) - highLightOffset) / gridScale,
                        ((obj.z - (obj.height / 2) + viewOffsetY) - highLightOffset) / gridScale,
                        ((obj.width + highLightOffset * 2) / gridScale),
                        ((obj.height + highLightOffset * 2) / gridScale)
                    );
                    if (!checkBoxAllActivationRange.Checked)
                    {
                        if (obj is Enemy)
                        {
                            Enemy enemy = (Enemy)obj;
                            e.Graphics.DrawEllipse(Pens.Red,
                                ((enemy.x -(enemy.width /2)  + viewOffsetX) - enemy.activationRange) / gridScale,
                                ((enemy.z -(enemy.height /2) + viewOffsetY) - enemy.activationRange) / gridScale,
                                ((enemy.width + enemy.activationRange * 2) / gridScale),
                                ((enemy.height  +enemy.activationRange * 2) / gridScale));
                        }
                    }
                }
                #endregion
            }
        }

        /// <summary>
        /// @Author: Andrew Seba
        /// @Description: Sets the the value to max or min if out of parameters.
        /// (Tried to replicate Unity3D Clamp function.) Super simple!!!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        float Clamp(float value, float max, float min)
        {
            if(value > max)
            {
                return max;
            }
            else if(value < min)
            {
                return min;
            }
            else
            {
                return value;
            }
        }

        float Clamp(float value)
        {
            if( value < 0)
            {
                return 0f;
            }
            else if(value > 1)
            {
                return 1f;
            }
            else
            {
                return value;
            }
        }

        public GameObject GetPoint(GameObject start, GameObject end, GameObject curve, float t)
        {
            t = Clamp(t);
            float oneMinusT = 1f - t;
            return oneMinusT * oneMinusT * start + 2f * oneMinusT * t * curve + t * t * end;
        }


        #region Mouse Actions

        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        //Mouse Down
        private void gridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectedTool = Tools.SELECT;
                foreach (GameObject obj in allObjects)
                {
                    RectangleF tempBounds = obj.bounds;
                    tempBounds.Height /= gridScale;
                    tempBounds.Width /= gridScale;

                    if (tempBounds.Contains(gridRelative))
                    {
                        selectedObj = obj;
                        UpdateObjectText();
                        break;
                    }
                    else
                    {
                        selectedObj = null;
                        labelObjName.Text = "nothing selected";
                    }
                }

                gridView.Focus();
            }
            if (selectedTool == Tools.MOVE)
            {

                //Do move things.
                if (selectedObj != null)
                {
                    selectedObj.x = SceneX;
                    selectedObj.z = SceneY;

                    if (snapToGrid)
                    {
                        selectedObj.x = (int)Math.Round(selectedObj.x);
                        selectedObj.z = (int)Math.Round(selectedObj.z);
                    }
                    //selectedObj.bounds = new RectangleF(selectedObj.x / gridScale, selectedObj.z / gridScale, selectedObj.width, selectedObj.height);
                    UpdateObjectText();
                    mouseDown = true;
                }
            }



            gridView.Refresh();

        }
        
        //Click
        private void gridView_Click(object sender, EventArgs e)
        {
            if (selectedTool == Tools.SELECT)
            {
                foreach (GameObject obj in allObjects)
                {
                    RectangleF tempBounds = obj.bounds;
                    tempBounds.Height /= gridScale;
                    tempBounds.Width /= gridScale;

                    if (tempBounds.Contains(gridRelative))
                    {
                        selectedObj = obj;
                        if(selectedObj is Enemy)
                        {
                            tabControlViewOptions.SelectTab(2);
                            groupBoxEnemyProperties.Visible = true;
                        }
                        else
                        {
                            groupBoxEnemyProperties.Visible = false;
                        }
                        UpdateObjectText();
                        break;
                    }
                    else
                    {
                        selectedObj = null;
                        selectedTimeNode = null;
                        groupBoxEnemyProperties.Visible = false;
                        labelObjName.Text = "nothing selected";
                    }
                }

                foreach(Movements movement in movements)
                {
                    if (movement.endWaypoint == selectedObj)
                    {
                        selectedTimeNode = movement;

                        switch(movement.moveType)
                        {
                            case MovementTypes.STRAIGHT:
                                groupBoxWait.Visible = false;
                                groupBoxMoveProperties.Visible = true;
                                groupBoxBezierProperties.Visible = false;

                                comboBoxWaypointType.SelectedIndex = 1;
                                numericUpDownMoveTime.Value = (movement.movementTime);
                                labelTargetNodeName.Text = movement.endWaypoint.ToString();
                                break;
                            case MovementTypes.BEZIER:
                                numericUpDownBezierTime.Value = movement.movementTime;
                                groupBoxWait.Visible = false;
                                groupBoxMoveProperties.Visible = false;
                                groupBoxBezierProperties.Visible = true;

                                comboBoxWaypointType.SelectedIndex = 2;
                                labelBezierEndNodeOutput.Text = movement.endWaypoint.ToString();
                                break;
                        }

                        groupBoxWaypointProperties.Enabled = true;

                        panelMovement.Refresh();
                        break;
                    }

                }
            }
            gridView.Focus();
            gridView.Refresh();

        }

        //Update relative mouse positions. and xy text of the mouse.
        private void gridView_MouseMove(object sender, MouseEventArgs e)
        {
            gridRelative = this.PointToClient(Cursor.Position);

            gridRelative.X -= (gridView.Location.X + 106 + groupBoxGridView.Margin.Left);
            gridRelative.Y -= (groupBoxGridView.Top + 45);

            SceneX = (gridRelative.X * gridScale) - viewOffsetX;
            SceneY = (gridRelative.Y * gridScale) - viewOffsetY;

            labelViewInfo.Text = gridRelative.ToString();
            labelGridViewMousePos.Text = (SceneX).ToString() + "," + (SceneY).ToString();

            if (mouseDown)
            {
                if(selectedObj != null)
                {
                    selectedObj.x = SceneX;
                    selectedObj.z = SceneY;

                    if (snapToGrid)
                    {
                        selectedObj.x = (int)Math.Round(selectedObj.x);
                        selectedObj.z = (int)Math.Round(selectedObj.z);
                    }

                    gridView.Refresh();
                    UpdateObjectText();
                }
            }
        }
        #endregion


        #region SaveFile
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*|Seba Runner Level (*.srl)|*.srl";
            saveFileDialog1.FilterIndex = 3;
            saveFileDialog1.RestoreDirectory = true;

            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if((myStream = saveFileDialog1.OpenFile()) != null)
                {
                    //Code to write the stream here.
                    StreamWriter sw = new StreamWriter(myStream);
                    sw.WriteLine("\"LVLNAME\"" + "\"" + levelName + "\"");
                    sw.WriteLine("\"AUTHOR\"" + "\"" + authorName + "\"");
                    sw.WriteLine("\"DATE\"" + "\"" + DateTime.Today.ToShortDateString() + "\"");

                    foreach(GameObject obj in allObjects)
                    {
                        if(obj is Crate)
                        {
                            sw.WriteLine("O_CRATE " + string.Format("{0},{1},{2}", obj.x,obj.y,obj.z));
                        }
                        if(obj is Node)
                        {
                            Node node = (Node)obj;
                            if (node.isPlayer)
                            {
                                sw.WriteLine("O_PLAYER " + string.Format("{0},{1},{2}", node.x, obj.y, obj.z));
                            }
                        }
                    }
                    foreach(Movements move in movements)
                    {
                        switch (move.moveType)
                        {
                            case MovementTypes.WAIT:
                                sw.WriteLine(string.Format("M_{0} {1}",
                                    move.moveType,
                                    move.movementTime));
                                break;
                            case MovementTypes.STRAIGHT:
                                sw.WriteLine(string.Format("M_{0} {1} {2},{3},{4}",
                                    move.moveType,
                                    move.movementTime,
                                    move.endWaypoint.x, move.endWaypoint.y,move.endWaypoint.z));
                                break;
                            case MovementTypes.BEZIER:
                                sw.WriteLine(string.Format("M_{0} {1} {2},{3},{4} {5},{6},{7}",
                                    move.moveType,
                                    move.movementTime,
                                    move.endWaypoint.x, move.endWaypoint.y, move.endWaypoint.z,
                                    move.curveWaypoint.x, move.curveWaypoint.y, move.curveWaypoint.z));
                                break;
                        }
                    }
                    sw.Close();
                    myStream.Close();
                }
            }
        }

        #endregion

        #region OpenFile
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You will lose any unsaved progress. Are you Sure?",
                "Open File",
                MessageBoxButtons.OKCancel);

            if(result == DialogResult.OK)
            {
                Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*|Seba Runner Level (*.srl)|*.srl";
                openFileDialog1.FilterIndex = 3;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if ((myStream = openFileDialog1.OpenFile()) != null)
                        {
                            allObjects = new List<GameObject>();
                            movements = new List<Movements>();

                            using (myStream)
                            {
                                StreamReader sr = new StreamReader(myStream);
                                readline = sr.ReadLine();

                                UInt16 loopCount = 0;
                                while (readline != null)
                                {
                                    loopCount++;
                                    Console.WriteLine("loop #" + loopCount);
                                    Console.WriteLine(readline);
                                    Movements tempMove;
                                    string[] coords;
                                    string[] keywords = readline.Split('_');
                                    Node tempNode;

                                    #region Movement Parsing
                                    if (keywords[0].ToUpper() == "M")
                                    {
                                        string[] words = keywords[1].Split(' ');
                                        switch ((MovementTypes)System.Enum.Parse(typeof(MovementTypes), words[0].ToUpper()))
                                        {
                                            case MovementTypes.STRAIGHT:
                                                tempMove = new Movements();
                                                tempMove.moveType = MovementTypes.STRAIGHT;
                                                tempMove.movementTime = (Decimal)Convert.ToDouble(words[1]);

                                                coords = words[2].Split(',');
                                                tempNode = new Node();
                                                tempNode.x = Convert.ToSingle(coords[0]);
                                                tempNode.y = Convert.ToSingle(coords[1]);
                                                tempNode.z = Convert.ToSingle(coords[2]);

                                                tempMove.endWaypoint = tempNode;
                                                AddObject(tempNode, false);
                                                movements.Add(tempMove);
                                                break;

                                            case MovementTypes.WAIT:
                                                tempMove = new Movements();
                                                tempMove.moveType = MovementTypes.WAIT;
                                                tempMove.movementTime = Convert.ToDecimal(words[1]);

                                                movements.Add(tempMove);
                                                break;

                                            case MovementTypes.BEZIER:
                                                tempMove = new Movements();
                                                tempMove.moveType = MovementTypes.BEZIER;
                                                tempMove.movementTime = Convert.ToDecimal(words[1]);

                                                tempNode = new Node();
                                                coords = words[2].Split(',');
                                                tempNode.x = Convert.ToSingle(coords[0]);
                                                tempNode.y = Convert.ToSingle(coords[1]);
                                                tempNode.z = Convert.ToSingle(coords[2]);
                                                tempMove.endWaypoint = tempNode;
                                                AddObject(tempNode, false);

                                                tempNode = new Node();
                                                coords = words[3].Split(',');
                                                tempNode.x = Convert.ToSingle(coords[0]);
                                                tempNode.y = Convert.ToSingle(coords[1]);
                                                tempNode.z = Convert.ToSingle(coords[2]);
                                                tempMove.curveWaypoint = tempNode;
                                                AddObject(tempNode, false);

                                                movements.Add(tempMove);
                                                break;
                                        }
                                    }
                                    #endregion

                                    //Next line
                                    readline = sr.ReadLine();
                                }

                                sr.Close();
                            }
                        }
                        myStream.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from dis. Original error: " + ex.Message);
                    }
                }

                gridView.Refresh();
                panelMovement.Refresh();
            }

        }

        #endregion

        #region Tool Controls
        private void checkSelectTool_CheckedChanged(object sender, EventArgs e)
        {
            selectedTool = Tools.SELECT;
            checkMoveTool.Checked = false;
        }

        private void checkMoveTool_CheckedChanged(object sender, EventArgs e)
        {
            selectedTool = Tools.MOVE;
            checkSelectTool.Checked = false;
        }

        #endregion

        #region GridView Control Tab
        //ZOOM BAR
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            gridScale = ((float)trackBar1.Value / 100);
            if(gridScale < 0.04)
            {
                currentGridLines = GridLines.ONE;
            }
            else if(gridScale < 0.06)
            {
                currentGridLines = GridLines.FIVE;
            }
            else if(gridScale < 0.1)
            {
                currentGridLines = GridLines.TEN;
            }
            labelZoomAmount.Text = "Zoom Amount: " + gridScale.ToString();

            //vScrollBar1.Maximum = (int)(maxWorldHeight / (decimal)gridScale);
            //hScrollBar1.Maximum = (int)(maxWorldHeight / (decimal)gridScale);
            gridView.Refresh();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            viewOffsetY = -e.NewValue * gridScale;
            gridView.Refresh();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            viewOffsetX = -e.NewValue * gridScale;
            gridView.Refresh();
        }

        #endregion

        #region World Controls Tab

        private void numericUpDownWorldMaximumHeight_ValueChanged(object sender, EventArgs e)
        {
            maxWorldHeight = numericUpDownWorldMaximumHeight.Value;
            vScrollBar1.Maximum = (int)((float)maxWorldHeight / gridScale);
            gridView.Refresh();
        }

        private void numericUpDownWorldMaximumWidth_ValueChanged(object sender, EventArgs e)
        {
            maxWorldWidth = numericUpDownWorldMaximumWidth.Value;
            hScrollBar1.Maximum = (int)((float)maxWorldWidth / gridScale);
            gridView.Refresh();
        }

        private void textBoxWorldName_TextChanged(object sender, EventArgs e)
        {
            if(textBoxWorldName.Text != "")
            {
                if(textBoxWorldName.Text.Contains(' '))
                    levelName = textBoxWorldName.Text.Remove(' ');
            }
        }

        #endregion

        void UpdateObjectText()
        {
            labelObjName.Text = "Name: " + selectedObj.name;
            labelObjPos.Text = "Position: " + selectedObj.x + "," + selectedObj.z;
        }

        private void checkBoxShaded_CheckedChanged(object sender, EventArgs e)
        {
            wireframe = checkBoxShaded.Checked;
            gridView.Refresh();
        }


        #region Tool Strip
        private void crateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Crate crate = new Crate();
            AddObject(crate);
        }

        private void waypointToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Node node = new Node();
            AddObject(node);
        }

        private void playerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(GameObject obj in allObjects)
            {
                if(obj is Node)
                {
                    Node node = (Node)obj;
                    if (node.isPlayer)
                    {
                        MessageBox.Show("Player Already in Scene!");
                    }
                }

            }
            Node newNode = new Node();
            newNode.isPlayer = true;
            newNode.name = "Player";
            newNode.fillColor = Brushes.DeepSkyBlue;
            newNode.outlineColor = Pens.LightSkyBlue;
            AddObject(newNode);

        }

        private void enemyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enemy enemy = new Enemy();
            enemy.fillColor = Brushes.IndianRed;
            enemy.outlineColor = Pens.MediumVioletRed;
            //Show object properties tab
            tabControlViewOptions.SelectTab(2);
            groupBoxEnemyProperties.Visible = true;
            numericUpDownEnemyActivationRange.Value = (decimal)enemy.activationRange;
            AddObject(enemy);
        }

        private void addNewEnemyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enemy enemy = new Enemy();
            enemy.fillColor = Brushes.IndianRed;
            enemy.outlineColor = Pens.MediumVioletRed;
            //Show object properties tab
            tabControlViewOptions.SelectTab(2);
            groupBoxEnemyProperties.Visible = true;
            numericUpDownEnemyActivationRange.Value = (decimal)enemy.activationRange;
            AddObject(enemy, true);
        }

        #endregion

        private void gridView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //Console.WriteLine(e.KeyCode);
            if (e.KeyCode == Keys.ControlKey)
            {
                snapToGrid = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                snapToGrid = false;
            }
        }

        /// <summary>
        /// @Description: Adds the object to the list with a unique name.
        /// </summary>
        /// <param name="pObj"></param>
        void AddObject(GameObject pObj)
        {
            int howManyCopies = 0;

            foreach (GameObject obj in allObjects)
            {
                if (obj.name.StartsWith(pObj.name))
                {
                    howManyCopies++;
                }
            }
            if(howManyCopies > 0)
            {
                pObj.name = pObj.name + " ( " + howManyCopies + " )";
            }

            allObjects.Add(pObj);
            pObj.x = (gridView.DisplayRectangle.Left + (gridView.DisplayRectangle.Width / 2)) * gridScale - viewOffsetX;
            pObj.z = (gridView.DisplayRectangle.Top + (gridView.DisplayRectangle.Height / 2)) * gridScale - viewOffsetY;

            selectedObj = pObj;
            gridView.Refresh();
        }

        void AddObject(GameObject pObj, bool MousePos)
        {
            int howManyCopies = 0;

            foreach (GameObject obj in allObjects)
            {
                if (obj.name.StartsWith(pObj.name))
                {
                    howManyCopies++;
                }
            }
            if (howManyCopies > 0)
            {
                pObj.name = pObj.name + " ( " + howManyCopies + " )";
            }

            allObjects.Add(pObj);

            if (MousePos)
            {
                pObj.x = SceneX;
                pObj.z = SceneY;
            }

            selectedObj = pObj;
            gridView.Refresh();
        }

        #region Grid Right Click
        private void addNewWaypointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Node node = new Node();
            AddObject(node, true);

        }

        private void newMovementNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Node node = new Node();
            AddObject(node, true);

            Movements tempMovement = new Movements();
            tempMovement.name = "Move";
            tempMovement.moveType = MovementTypes.STRAIGHT;
            tempMovement.movementTime = 1.0M;
            tempMovement.endWaypoint = node;

            movements.Add(tempMovement);
            selectedTimeNode = tempMovement;
            panelMovement.Refresh();
            gridView.Refresh();
        }
        #endregion

        //Add new Movement
        private void buttonAddMovement_Click(object sender, EventArgs e)
        {
            Movements tempMovement = new Movements();
            tempMovement.name = "Move";
            tempMovement.moveType = MovementTypes.WAIT;
            tempMovement.movementTime = 1.0M;
            tempMovement.endWaypoint = null;

            movements.Add(tempMovement);
            panelMovement.Refresh();
        }

        private void panelMovement_Paint(object sender, PaintEventArgs e)
        {
            float buttonOffset = 0;
            float buttonSpacing = 2;
            int horizontalScale = 10;

            Pen outline = Pens.PeachPuff;
            Brush fill = Brushes.Peru;

            foreach(Movements move in movements)
            {
                //Font font = new Font("Arial", 7);
                if(selectedTimeNode is Movements && move == (Movements)selectedTimeNode)
                {
                    outline = Pens.LightBlue;
                    fill = Brushes.Blue;
                }
                else
                {
                    outline = Pens.PeachPuff;
                    fill = Brushes.Peru;
                }

                RectangleF tempBounds = new RectangleF(
                    panelMovement.DisplayRectangle.Left + buttonOffset,
                    panelMovement.DisplayRectangle.Top,
                    (float)move.movementTime * horizontalScale,
                    panelMovement.DisplayRectangle.Height);

                //Rectangle
                e.Graphics.FillRectangle(
                    fill,
                    tempBounds.X,
                    tempBounds.Y,
                    tempBounds.Width,
                    tempBounds.Height);
                //Outline
                e.Graphics.DrawRectangle( 
                    outline,
                    tempBounds.X,
                    tempBounds.Y,
                    tempBounds.Width,
                    tempBounds.Height);

                move.bounds = tempBounds;
                //e.Graphics.DrawString(move.name, font, Brushes.PeachPuff, panelMovement.DisplayRectangle.Left, panelMovement.DisplayRectangle.Top + panelMovement.DisplayRectangle.Height / 2);

                buttonOffset += ((float)move.movementTime * horizontalScale) + buttonSpacing;
            }
        }

        private void panelMovement_MouseMove(object sender, MouseEventArgs e)
        {
            moveTimeRelative = panelMovement.PointToClient(Cursor.Position);

            //float moveTimeMouseX = moveTimeRelative.X -= panelMovement.DisplayRectangle.Left + 319;
            //float moveTimeMouseY = moveTimeRelative.Y -= panelMovement.DisplayRectangle.Top + gridView.Height + 87;

            //Console.WriteLine(moveTimeRelative.X);
            //Console.WriteLine(moveTimeRelative.Y);
        }

        private void panelMovement_MouseClick(object sender, MouseEventArgs e)
        {
            foreach (Movements movement in movements)
            {
                if (movement.bounds.Contains(moveTimeRelative))
                {
                    selectedTimeNode = movement;
                    break;
                }
                selectedTimeNode = null;
            }

            if(selectedTimeNode is Movements)
            {
                Movements movement = (Movements)selectedTimeNode;
                textWaypointName.Text = movement.name;
                switch (movement.moveType)
                {
                    case MovementTypes.WAIT:
                        groupBoxWait.Visible = true;
                        groupBoxMoveProperties.Visible = false;
                        groupBoxBezierProperties.Visible = false;
                        comboBoxWaypointType.SelectedIndex = 0;

                        numericUpDownWaitTime.Value = Convert.ToDecimal(movement.movementTime);
                        break;
                    case MovementTypes.STRAIGHT:
                        groupBoxWait.Visible = false;
                        groupBoxMoveProperties.Visible = true;
                        groupBoxBezierProperties.Visible = false;
                        comboBoxWaypointType.SelectedIndex = 1;
                        labelTargetNodeName.Text = movement.endWaypoint.ToString();
                        numericUpDownMoveTime.Value = Convert.ToDecimal(movement.movementTime);
                        selectedObj = movement.endWaypoint;
                        break;
                    case MovementTypes.BEZIER:
                        groupBoxBezierProperties.Visible = true;
                        groupBoxWait.Visible = false;
                        groupBoxMoveProperties.Visible = false;

                        comboBoxWaypointType.SelectedIndex = 2;

                        numericUpDownBezierTime.Value = movement.movementTime;
                        break;
                }
                

                groupBoxWaypointProperties.Enabled = true;
            }
            else
            {
                selectedTimeNode = null;
                groupBoxWaypointProperties.Enabled = false;
            }

            Refresh();
        }

        private void comboBoxWaypointType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(selectedTimeNode is Movements)
            {
                Movements movement = (Movements)selectedTimeNode;
                switch (comboBoxWaypointType.SelectedIndex)
                {
                    case 0:
                        movement.moveType = MovementTypes.WAIT;
                        groupBoxMoveProperties.Visible = false;
                        groupBoxBezierProperties.Visible = false;
                        groupBoxWait.Visible = true;

                        numericUpDownWaitTime.Value = Convert.ToDecimal(movement.movementTime);
                        break;
                    case 1:
                        movement.moveType = MovementTypes.STRAIGHT;
                        groupBoxWait.Visible = false;
                        groupBoxBezierProperties.Visible = false;
                        groupBoxMoveProperties.Visible = true;

                        numericUpDownMoveTime.Value = Convert.ToDecimal(movement.movementTime);
                        break;
                    case 2:
                        movement.moveType = MovementTypes.BEZIER;
                        groupBoxWait.Visible = false;
                        groupBoxMoveProperties.Visible = false;
                        groupBoxBezierProperties.Visible = true;

                        if(movement.curveWaypoint == null)
                        {
                            Node tempCurveNode = new Node();
                            movement.curveWaypoint = tempCurveNode;
                            AddObject(tempCurveNode);
                            gridView.Refresh();
                        }

                        numericUpDownBezierTime.Value = movement.movementTime;
                        break;
                }
            }
            else if(selectedTimeNode is Effects)
            {

            }
            else if(selectedTimeNode is Facings)
            {

            }
            Refresh();

        }

        private void numericUpDownWaitTime_ValueChanged(object sender, EventArgs e)
        {
            if(selectedTimeNode is Movements)
            {
                Movements movement = (Movements)selectedTimeNode;
                movement.movementTime = numericUpDownWaitTime.Value;
                panelMovement.Refresh();
            }
        }

        private void numericUpDownMoveTime_ValueChanged(object sender, EventArgs e)
        {
            if(selectedTimeNode is Movements)
            {
                Movements movement = (Movements)selectedTimeNode;
                movement.movementTime = numericUpDownMoveTime.Value;
                panelMovement.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            nodePicker.listBoxNodes.Items.Clear();
            foreach (GameObject obj in allObjects)
            {
                if (obj is Node)
                {
                    Node node = (Node)obj;
                    if (!node.isPlayer)
                    {
                        nodePicker.listBoxNodes.Items.Add(obj);
                    }
                }

            }
            nodePicker.Show();
        }

        public void AssignNode()
        {
            if(selectedTimeNode is Movements)
            {
                Movements movement = (Movements)selectedTimeNode;
                Node endNode = (Node)nodePicker.listBoxNodes.Items[nodePicker.selectedNodeIndex];
                movement.endWaypoint = endNode;
                switch (movement.moveType)
                {
                    case MovementTypes.STRAIGHT:
                        labelTargetNodeName.Text = endNode.name;
                        break;
                    case MovementTypes.BEZIER:
                        labelBezierEndNodeOutput.Text = endNode.name;
                        break;

                }
            }
            Refresh();
        }

        private void numericUpDownEnemyActivationRange_ValueChanged(object sender, EventArgs e)
        {
            if(selectedObj is Enemy)
            {
                Enemy enemy = (Enemy)selectedObj;
                enemy.activationRange = (float)numericUpDownEnemyActivationRange.Value;
                gridView.Refresh();
            }
        }

        private void buttonBezierEndNodeSelector_Click(object sender, EventArgs e)
        {
            nodePicker.listBoxNodes.Items.Clear();
            foreach (GameObject obj in allObjects)
            {
                if (obj is Node)
                {
                    Node node = (Node)obj;
                    if (!node.isPlayer)
                    {
                        nodePicker.listBoxNodes.Items.Add(obj);
                    }
                }
                    
            }
            nodePicker.Show();
        }

        private void numericUpDownBezierTime_ValueChanged(object sender, EventArgs e)
        {
            if (selectedTimeNode is Movements)
            {
                Movements movement = (Movements)selectedTimeNode;
                movement.movementTime = numericUpDownBezierTime.Value;
                panelMovement.Refresh();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You will lose any unsaved progress. Are you Sure?",
                "New File",
                MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                allObjects = new List<GameObject>();
                movements = new List<Movements>();
                facings = new List<Facings>();
                effects = new List<Effects>();
            }
            Refresh();
        }
    }
}
