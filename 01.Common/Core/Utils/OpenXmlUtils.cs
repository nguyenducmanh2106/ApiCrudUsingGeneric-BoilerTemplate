using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Drawing.Pictures;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Wordprocessing;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using System.Collections;
using System.Linq;
using System.Data;

namespace Core.Utils
{
    public static class OpenXmlUtils
    {
        private static int DefaultFontSize = 12;
        public static int DefaultFontSize_14 = 14;
        private static RunProperties DefaultRunProperties(int fontSize)
        {
            return DefaultRunProperties(fontSize, false);
        }
        private static RunProperties DefaultRunProperties(int fontSize, string textAlign)
        {
            return DefaultRunProperties1(fontSize, textAlign);
        }

        private static RunProperties DefaultRunProperties(int fontSize, bool IsTitle)
        {
            RunProperties property = new RunProperties();
            FontSize fs = new FontSize();
            fs.Val = new StringValue((fontSize * 2).ToString());
            property.FontSize = fs;
            RunFonts rf = new RunFonts();
            rf.Ascii = new StringValue("Times new Roman");
            rf.HighAnsi = rf.Ascii;
            property.RunFonts = rf;
            Languages lang = new Languages();
            lang.Val = "vi-VN";
            property.Languages = lang;
            if (IsTitle)
            {
                Bold bold = new Bold();
                bold.Val = new OnOffValue(true);
                property.Bold = bold;
            }
            return property;
        }

        private static RunProperties DefaultRunProperties1(int fontSize, string textAlign)
        {
            RunProperties property = new RunProperties();
            FontSize fs = new FontSize();
            fs.Val = new StringValue((fontSize * 2).ToString());
            property.FontSize = fs;
            RunFonts rf = new RunFonts();
            rf.Ascii = new StringValue("Times new Roman");
            rf.HighAnsi = rf.Ascii;
            property.RunFonts = rf;
            Languages lang = new Languages();
            lang.Val = "vi-VN";
            property.Languages = lang;
            property.Append(new Justification()
            {
                Val = JustificationValues.Right
            });
            return property;
        }

        private static ParagraphProperties DefaultParagraphProperties()
        {
            return DefaultParagraphProperties(false);
        }

        private static ParagraphProperties DefaultParagraphProperties(bool FirsLineOnly)
        {
            ParagraphProperties property = new ParagraphProperties();
            StringValue sv = new StringValue();
            Indentation ind = new Indentation();
            if (FirsLineOnly)
            {
                ind.FirstLine = sv;
            }
            else
            {
                ind.Left = sv;
            }

            property.Indentation = ind;
            return property;
        }

        private static void AddTextToSdt(SdtElement sdt, string text)
        {
            AddTextToSdt(sdt, text, DefaultFontSize);
        }

        private static void AddTextToSdt(SdtElement sdt, string text, string textAlign)
        {
            AddTextToSdt(sdt, text, DefaultFontSize, textAlign);
        }

        private static void AddTextToSdt_Font(SdtElement sdt, string text)
        {
            AddTextToSdt(sdt, text, DefaultFontSize_14);
        }

        private static void AddTextToSdt_Title(SdtElement sdt, string text)
        {
            AddTextToSdt(sdt, text, true, DefaultFontSize_14);
        }

        public static void AddImageToSdt(SdtElement sdt, string text, long width, long height)
        {
            if (sdt == null) return;
            Drawing element = new Drawing(new DW.Inline(new DW.Extent()
            {
                Cx = width,
                Cy = height
            }, new DW.EffectExtent()
            {
                LeftEdge = 0L,
                TopEdge = 0L,
                RightEdge = 0L,
                BottomEdge = 0L
            }, new DW.DocProperties()
            {
                Id = (UInt32Value)1U,
                Name = "Picture 1"
            }, new DW.NonVisualGraphicFrameDrawingProperties(new DocumentFormat.OpenXml.Drawing.GraphicFrameLocks()
            {
                NoChangeAspect = true
            }), new DocumentFormat.OpenXml.Drawing.Graphic(new DocumentFormat.OpenXml.Drawing.GraphicData(new PIC.Picture(new PIC.NonVisualPictureProperties(new PIC.NonVisualDrawingProperties()
            {
                Id = (UInt32Value)0U,
                Name = "new Bitmap Image.jpg"
            }, new PIC.NonVisualPictureDrawingProperties()), new PIC.BlipFill(new DocumentFormat.OpenXml.Drawing.Blip(new DocumentFormat.OpenXml.Drawing.BlipExtensionList(new DocumentFormat.OpenXml.Drawing.BlipExtension()
            {
                Uri = "{28A0092B-C50C-407E-A947-70E740481C1C}"
            }))
            {
                Embed = text,
                CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print
            }, new DocumentFormat.OpenXml.Drawing.Stretch(new DocumentFormat.OpenXml.Drawing.FillRectangle())), new PIC.ShapeProperties(new DocumentFormat.OpenXml.Drawing.Transform2D(new DocumentFormat.OpenXml.Drawing.Offset()
            {
                X = 0L,
                Y = 0L
            }, new DocumentFormat.OpenXml.Drawing.Extents()
            {
                Cx = width,
                Cy = height
            }), new DocumentFormat.OpenXml.Drawing.PresetGeometry(new DocumentFormat.OpenXml.Drawing.AdjustValueList())
            {
                Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle
            })))
            {
                Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"
            }))
            {
                DistanceFromTop = (UInt32Value)0U,
                DistanceFromBottom = (UInt32Value)0U,
                DistanceFromLeft = (UInt32Value)0U,
                DistanceFromRight = (UInt32Value)0U,
                EditId = "50D07946"
            });

            Paragraph p = new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(element));
            OpenXmlElement parent = sdt.Parent;
            parent.ReplaceChild(p, sdt);

        }

        public static void AddTextToSdtWithoutFont(SdtElement sdt, String text)
        {
            if (sdt == null)
                return;
            if (sdt.GetType() == typeof(SdtCell))
            {
                Run nRun = new Run(new Text(text));
                Paragraph p = new Paragraph(nRun);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
            else if (sdt.GetType() == typeof(SdtRun))
            {
                Run nRun = new Run(new Text(text));
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(nRun, sdt);
            }
            else if (sdt.GetType() == typeof(SdtBlock))
            {
                Run nRun = new Run(new Text(text));
                Paragraph p = new Paragraph(nRun);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
        }

        public static void AddTextToSdt(SdtElement sdt, string text, int fontSize)
        {
            if (sdt == null) return;
            if (sdt.GetType() == typeof(SdtCell))
            {
                Run nRun = new Run(new Text(text));
                nRun.RunProperties = DefaultRunProperties(fontSize);
                Paragraph p = new Paragraph(nRun);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
            else if (sdt.GetType() == typeof(SdtRun))
            {
                Run nRun = new Run(new Text(text));
                nRun.RunProperties = DefaultRunProperties(fontSize);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(nRun, sdt);
            }
            else if (sdt.GetType() == typeof(SdtBlock))
            {
                Run nRun = new Run(new Text(text));
                nRun.RunProperties = DefaultRunProperties(fontSize);
                Paragraph p = new Paragraph(nRun);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
        }
        public static void AddTextToSdt(SdtElement sdt, string text, int fontSize, string textAlign)
        {
            if (sdt == null) return;
            if (sdt.GetType() == typeof(SdtCell))
            {
                Run nRun = new Run(new Text(text));
                nRun.RunProperties = DefaultRunProperties(fontSize, textAlign);
                Paragraph p = new Paragraph(nRun);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
            else if (sdt.GetType() == typeof(SdtRun))
            {
                Run nRun = new Run(new Text(text));
                nRun.RunProperties = DefaultRunProperties(fontSize, textAlign);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(nRun, sdt);
            }
            else if (sdt.GetType() == typeof(SdtBlock))
            {
                Run nRun = new Run(new Text(text));
                nRun.RunProperties = DefaultRunProperties(fontSize, textAlign);
                Paragraph p = new Paragraph(nRun);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
        }

        public static void AddTextToSdt(SdtElement sdt, string text, bool IsTitle, int fontSize)
        {
            if (sdt == null) return;
            if (sdt.GetType() == typeof(SdtCell))
            {
                Run nRun = new Run(new Text(text));
                nRun.RunProperties = DefaultRunProperties(fontSize, IsTitle);
                Paragraph p = new Paragraph(nRun);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
            else if (sdt.GetType() == typeof(SdtRun))
            {
                Run nRun = new Run(new Text(text));
                nRun.RunProperties = DefaultRunProperties(fontSize, IsTitle);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(nRun, sdt);
            }
            else if (sdt.GetType() == typeof(SdtBlock))
            {
                Run nRun = new Run(new Text(text));
                nRun.RunProperties = DefaultRunProperties(fontSize, IsTitle);
                Paragraph p = new Paragraph(nRun);
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
        }

        public static void AddTextToSdt(SdtElement sdt, string[] text)
        {
            AddTextToSdt(sdt, text, DefaultFontSize);
        }

        public static void AddTextToSdt(SdtElement sdt, string[] text, int fontSize)
        {
            if (sdt == null) return;
            if (sdt.GetType() == typeof(SdtCell))
            {
                Run nRun;
                Paragraph p = new Paragraph();
                for (int index = 0; index < text.Length; index++)
                {
                    nRun = new Run(new Text(text[index]));
                    nRun.RunProperties = DefaultRunProperties(fontSize);
                    p.Append(nRun);
                    p.Append(new Break());
                }

                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
            else if (sdt.GetType() == typeof(SdtRun))
            {

            }
            else if (sdt.GetType() == typeof(SdtBlock))
            {
                Run nRun;
                Paragraph p = new Paragraph();
                p.ParagraphProperties = DefaultParagraphProperties();

                for (int index = 0; index < text.Length; index++)
                {
                    nRun = new Run(new Text(text[index]));
                    nRun.RunProperties = DefaultRunProperties(fontSize);
                    p.Append(nRun);
                    p.Append(new Break());
                }
                OpenXmlElement parent = sdt.Parent;
                parent.ReplaceChild(p, sdt);
            }
        }

        public static void AddTextToSdtBlock(SdtElement sdt, string[] text)
        {
            AddTextToSdtBlock(sdt, text, DefaultFontSize);
        }

        public static void AddTextToSdtBlock(SdtElement sdt, string[] text, int fontSize)
        {
            AddTextToSdtBlock(sdt, text, false, fontSize);
        }

        public static void AddTextToSdtBlock(SdtElement sdt, string[] text, bool IsTitle, int fontSize)
        {
            if (sdt == null) return;
            sdt.SdtProperties.RemoveAllChildren<SdtPlaceholder>();
            Run nRun;
            Paragraph p = new Paragraph();
            if (IsTitle)
            {
                ParagraphProperties property = new ParagraphProperties();
                Justification jc = new Justification();
                jc.Val = JustificationValues.Center;
                property.Justification = jc;
                p.ParagraphProperties = property;
            }
            else
            {
                p.ParagraphProperties = DefaultParagraphProperties();
            }

            for (int index = 0; index < text.Length; index++)
            {
                nRun = new Run(new Text(text[index]));
                nRun.RunProperties = DefaultRunProperties(fontSize, IsTitle);
                p.Append(nRun);
                p.Append(new Break());

                OpenXmlElement parent = sdt.Parent;
                if (sdt.GetType() == typeof(SdtBlock))
                {
                    parent.ReplaceChild(p, sdt);
                }
                else
                {
                    sdt.Parent.InsertBeforeSelf(p);
                    sdt.Remove();
                }
            }
        }

        public static void AddTextToSdtBlock(SdtElement sdt, string[] text, bool IsTitle, int fontSize, bool isBreak)
        {
            if (sdt == null) return;
            sdt.SdtProperties.RemoveAllChildren<SdtPlaceholder>();
            Run nRun;
            Paragraph p = new Paragraph();
            if (IsTitle)
            {
                ParagraphProperties property = new ParagraphProperties();
                Justification jc = new Justification();
                jc.Val = JustificationValues.Center;
                property.Justification = jc;
                p.ParagraphProperties = property;
            }
            else
            {
                p.ParagraphProperties = DefaultParagraphProperties();
            }
            for (int index = 0; index < text.Length; index++)
            {
                nRun = new Run(new Text(text[index]));
                nRun.RunProperties = DefaultRunProperties(fontSize, IsTitle);
                p.Append(nRun);

                if (isBreak)
                {
                    p.Append(new Break());
                }
            }
            OpenXmlElement parent = sdt.Parent;

            if (sdt.GetType() == typeof(SdtBlock))
            {
                parent.ReplaceChild(p, sdt);
            }
            else
            {
                sdt.Parent.InsertBeforeSelf(p);
                sdt.Remove();
            }
        }

        public static void WDAddTable(string fileName, string[,] data)
        {
            using (var document = WordprocessingDocument.Open(fileName, true))
            {
                var doc = document.MainDocumentPart.Document;
                Table table = new Table();
                TableProperties props = new TableProperties(new TableBorders(new TopBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                }, new BottomBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                }, new LeftBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                }, new RightBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                }, new InsideHorizontalBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                }, new InsideVerticalBorder()
                {
                    Val = new EnumValue<BorderValues>(BorderValues.Single),
                    Size = 12
                }));
                table.AppendChild<TableProperties>(props);

                for (var i = 0; i <= data.GetUpperBound(0); i++)
                {
                    var tr = new TableRow();

                    for (var j = 0; j <= data.GetUpperBound(1); j++)
                    {
                        var tc = new TableCell();
                        tc.Append(new Paragraph(new Run(new Text(data[i, j]))));
                        tc.Append(new TableCellProperties(new TableCellWidth()
                        {
                            Type = TableWidthUnitValues.Auto
                        }));
                        tr.Append(tc);
                    }

                    table.Append(tr);
                }

                doc.Body.Append(table);
                doc.Save();
            }
        }

        public static void AddTableToSdt(SdtElement sdt, System.Data.DataTable table, bool IsShowHeader)
        {
            AddTableToSdt(sdt, table, IsShowHeader, DefaultFontSize, false);
        }

        public static void AddTableToSdt(SdtElement sdt, System.Data.DataTable table, bool IsShowHeader, bool withParagraphProperty)
        {
            AddTableToSdt(sdt, table, IsShowHeader, withParagraphProperty);
        }

        public static void AddTableToSdt(SdtElement sdt, System.Data.DataTable table, bool IsShowHeader, int fontSize, bool withParagraphProperty)
        {
            if (sdt == null)
                return;
            if (table == null)
                return;
            ArrayList cellText = new ArrayList();
            Table tbl = new Table();
            TableProperties tableProperties1 = new TableProperties();
            TableStyle tableStyle1 = new TableStyle()
            {
                Val = "TableGrid"
            };
            TableWidth tableWidth1 = new TableWidth()
            {
                Width = "5000",
                Type = TableWidthUnitValues.Pct
            };
            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder()
            {
                Val = BorderValues.None,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            BottomBorder bottomBorder1 = new BottomBorder()
            {
                Val = BorderValues.None,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            LeftBorder leftBorder1 = new LeftBorder()
            {
                Val = BorderValues.None,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            RightBorder rightBorder1 = new RightBorder()
            {
                Val = BorderValues.None,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder()
            {
                Val = BorderValues.None,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder()
            {
                Val = BorderValues.None,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            tableBorders1.Append(topBorder1);
            tableBorders1.Append(bottomBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);
            TableLook tableLook1 = new TableLook()
            {
                Val = "04A0",
                FirstRow = true,
                LastRow = false,
                FirstColumn = true,
                LastColumn = false,
                NoHorizontalBand = false,
                NoVerticalBand = true
            };
            tableProperties1.Append(tableStyle1);
            tableProperties1.Append(tableWidth1);
            tableProperties1.Append(tableBorders1);
            tableProperties1.Append(tableLook1);
            tbl.Append(tableProperties1);

            for (int rowIndex = 0; rowIndex <= table.Rows.Count - 1; rowIndex++)
            {
                cellText.Add(Convert.ToString(table.Rows[rowIndex][0]));
                TableRow tr = CreateRow(cellText, fontSize, withParagraphProperty);
                tbl.AppendChild<TableRow>(tr);
                cellText = new ArrayList();
            }

            OpenXmlElement parent = sdt.Parent;
            parent.InsertAfter(tbl, sdt);
            sdt.Remove();
        }

        public static void AddTableStdNew(SdtElement sdt, System.Data.DataTable _table, bool IsShowHeader, string[] Headers)
        {
            if (sdt == null)
                return;
            if (_table == null)
                return;
            Table table = new Table();
            TableProperties tblProp = new TableProperties(new TableBorders(new TopBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new BottomBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new LeftBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new RightBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new InsideHorizontalBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new InsideVerticalBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }));
            table.AppendChild<TableProperties>(tblProp);

            if (IsShowHeader)
                table.Append(CreateNoHeader(_table, Headers));

            if (_table.Rows.Count > 0)
            {
                for (int i = 0; i <= _table.Rows.Count - 1; i++)
                {
                    TableRow tr = new TableRow();

                    for (int j = 0; j <= _table.Columns.Count - 1; j++)
                    {
                        TableCell tc1 = new TableCell();
                        tc1.Append(new TableCellProperties(new TableCellWidth()
                        {
                            Type = TableWidthUnitValues.Dxa,
                            Width = "4800"
                        }));

                        //if (!IsNoN(_table.Rows[i][j]))
                        if (_table.Rows[i][j] != null)
                            tc1.Append(new Paragraph(new Run(new Text(Convert.ToString(_table.Rows[i][j])))));
                        else
                            tc1.Append(new Paragraph(new Run(new Text(""))));
                        tr.Append(tc1);
                    }

                    table.Append(tr);
                }
            }

            OpenXmlElement parent = sdt.Parent;
            parent.InsertAfter(table, sdt);
            sdt.Remove();
        }

        public static void AddTableStdNew(SdtElement sdt, System.Data.DataTable _table, bool IsShowHeader, string[] Headers, string wTable)
        {
            if (sdt == null)
                return;
            if (_table == null)
                return;
            Table table = new Table();
            TableProperties tblProp = new TableProperties(new TableBorders(new TopBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new BottomBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new LeftBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new RightBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new InsideHorizontalBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new InsideVerticalBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }));
            table.AppendChild<TableProperties>(tblProp);

            if (IsShowHeader)
                table.Append(CreateNoHeader(_table, Headers));

            if (_table.Rows.Count > 0)
            {
                for (int i = 0; i <= _table.Rows.Count - 1; i++)
                {
                    TableRow tr = new TableRow();

                    for (int j = 0; j <= _table.Columns.Count - 1; j++)
                    {
                        TableCell tc1 = new TableCell();
                        tc1.Append(new TableCellProperties(new TableCellWidth()
                        {
                            Type = TableWidthUnitValues.Dxa,
                            Width = wTable
                        }));

                        //if (!IsNoN(_table.Rows[i][j]))
                        if (_table.Rows[i][j] != null)
                        {
                            Run nRun = new Run(new Text(Convert.ToString(_table.Rows[i][j])));
                            tc1.AppendChild(new Paragraph(nRun));
                        }
                        else
                            tc1.Append(new Paragraph(new Run(new Text(""))));

                        tr.Append(tc1);
                    }

                    table.Append(tr);
                }
            }

            OpenXmlElement parent = sdt.Parent;
            parent.InsertAfter(table, sdt);
            sdt.Remove();
        }

        public static void AddTableStdNew(SdtElement sdt, System.Data.DataTable _table, bool IsShowHeader, string[] Headers, string wTable, int fontSize)
        {
            if (sdt == null)
                return;
            if (_table == null)
                return;
            Table table = new Table();
            TableProperties tblProp = new TableProperties(new TableBorders(new TopBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new BottomBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new LeftBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new RightBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new InsideHorizontalBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }, new InsideVerticalBorder()
            {
                Val = new EnumValue<BorderValues>(BorderValues.BasicThinLines),
                Size = 1
            }));
            table.AppendChild<TableProperties>(tblProp);

            if (IsShowHeader)
                table.Append(CreateNoHeader(_table, Headers, fontSize));

            if (_table.Rows.Count > 0)
            {
                for (int i = 0; i <= _table.Rows.Count - 1; i++)
                {
                    TableRow tr = new TableRow();

                    for (int j = 0; j <= _table.Columns.Count - 1; j++)
                    {
                        TableCell tc1 = new TableCell();
                        tc1.Append(new TableCellProperties(new TableCellWidth()
                        {
                            Type = TableWidthUnitValues.Dxa,
                            Width = wTable
                        }));

                        //if (!IsNoN(_table.Rows[i][j]))
                        if (_table.Rows[i][j] != null)
                        {
                            Run nRun = new Run(new Text(Convert.ToString(_table.Rows[i][j])));
                            nRun.RunProperties = DefaultRunProperties(fontSize, false);
                            tc1.AppendChild(new Paragraph(nRun));
                        }
                        else
                            tc1.Append(new Paragraph(new Run(new Text(""))));

                        tr.Append(tc1);
                    }

                    table.Append(tr);
                }
            }

            OpenXmlElement parent = sdt.Parent;
            parent.InsertAfter(table, sdt);
            sdt.Remove();
        }

        public static void AddTableToSdt(SdtElement sdt, System.Data.DataTable table, bool IsShowHeader, string HeaderFileResource)
        {
            if (sdt == null)
                return;
            if (table == null)
                return;
            ArrayList cellText = new ArrayList();
            Table tbl = new Table();
            TableProperties tableProperties1 = new TableProperties();
            TableStyle tableStyle1 = new TableStyle()
            {
                Val = "TableGrid"
            };
            TableWidth tableWidth1 = new TableWidth()
            {
                Width = "5000",
                Type = TableWidthUnitValues.Pct
            };
            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            LeftBorder leftBorder1 = new LeftBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            RightBorder rightBorder1 = new RightBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            tableBorders1.Append(topBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);
            TableLook tableLook1 = new TableLook()
            {
                Val = "04A0",
                FirstRow = true,
                LastRow = false,
                FirstColumn = true,
                LastColumn = false,
                NoHorizontalBand = false,
                NoVerticalBand = true
            };
            tableProperties1.Append(tableStyle1);
            tableProperties1.Append(tableWidth1);
            tableProperties1.Append(tableBorders1);
            tableProperties1.Append(tableLook1);
            tbl.Append(tableProperties1);

            if (IsShowHeader)
                tbl.Append(CreateHeader(table, HeaderFileResource));

            for (int rowIndex = 0; rowIndex <= table.Rows.Count - 1; rowIndex++)
            {
                for (int cellIndex = 0; cellIndex <= table.Columns.Count - 1; cellIndex++)
                    cellText.Add(Convert.ToString(table.Rows[rowIndex][cellIndex]));

                TableRow tr = CreateRow(cellText);
                tbl.AppendChild<TableRow>(tr);
                cellText = new ArrayList();
            }

            OpenXmlElement parent = sdt.Parent;
            parent.InsertAfter(tbl, sdt);
            sdt.Remove();
        }

        public static void AddTableToSdtNoHeader(SdtElement sdt, System.Data.DataTable table, bool IsShowHeader, string[] Headers)
        {
            AddTableToSdtNoHeader(sdt, table, IsShowHeader, Headers, DefaultFontSize);
        }

        public static void AddTableToSdtNoHeader(SdtElement sdt, System.Data.DataTable table, bool IsShowHeader, string[] Headers, int fontSize)
        {
            if (sdt == null)
                return;
            if (table == null)
                return;
            ArrayList cellText = new ArrayList();
            Table tbl = new Table();
            TableProperties tableProperties1 = new TableProperties();
            TableStyle tableStyle1 = new TableStyle()
            {
                Val = "TableGrid"
            };
            TableWidth tableWidth1 = new TableWidth()
            {
                Width = "5000",
                Type = TableWidthUnitValues.Pct
            };
            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            BottomBorder bottomBorder1 = new BottomBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            LeftBorder leftBorder1 = new LeftBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            RightBorder rightBorder1 = new RightBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            tableBorders1.Append(topBorder1);
            tableBorders1.Append(bottomBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);
            TableLook tableLook1 = new TableLook()
            {
                Val = "04A0",
                FirstRow = true,
                LastRow = true,
                FirstColumn = true,
                LastColumn = true,
                NoHorizontalBand = false,
                NoVerticalBand = true
            };
            tableProperties1.Append(tableStyle1);
            tableProperties1.Append(tableWidth1);
            tableProperties1.Append(tableBorders1);
            tableProperties1.Append(tableLook1);
            tbl.Append(tableProperties1);

            if (IsShowHeader)
                tbl.Append(CreateNoHeader(table, Headers, fontSize));

            RunStyle rs = new RunStyle();

            for (int rowIndex = 0; rowIndex <= table.Rows.Count - 1; rowIndex++)
            {
                for (int cellIndex = 0; cellIndex <= table.Columns.Count - 1; cellIndex++)
                    cellText.Add(Convert.ToString(table.Rows[rowIndex][cellIndex]));

                TableRow tr = CreateRow(cellText, fontSize);
                tbl.AppendChild<TableRow>(tr);
                cellText = new ArrayList();
            }

            OpenXmlElement parent = sdt.Parent;
            parent.InsertAfter(tbl, sdt);
            sdt.Remove();
        }

        public static void AddTableToSdtNoHeader(MainDocumentPart mainPart, SdtElement sdt, System.Data.DataTable table, bool IsShowHeader, string[] Headers)
        {
            if (sdt == null)
                return;
            if (table == null)
                return;
            ArrayList cellText = new ArrayList();
            Table tbl = new Table();
            TableProperties tableProperties1 = new TableProperties();
            TableStyle tableStyle1 = new TableStyle()
            {
                Val = "TableGrid"
            };
            TableWidth tableWidth1 = new TableWidth()
            {
                Width = "5000",
                Type = TableWidthUnitValues.Pct
            };
            TableBorders tableBorders1 = new TableBorders();
            TopBorder topBorder1 = new TopBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            LeftBorder leftBorder1 = new LeftBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            RightBorder rightBorder1 = new RightBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            InsideHorizontalBorder insideHorizontalBorder1 = new InsideHorizontalBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            InsideVerticalBorder insideVerticalBorder1 = new InsideVerticalBorder()
            {
                Val = BorderValues.Single,
                Color = "auto",
                Size = (UInt32Value)0U,
                Space = (UInt32Value)0U
            };
            tableBorders1.Append(topBorder1);
            tableBorders1.Append(leftBorder1);
            tableBorders1.Append(rightBorder1);
            tableBorders1.Append(insideHorizontalBorder1);
            tableBorders1.Append(insideVerticalBorder1);
            TableLook tableLook1 = new TableLook()
            {
                Val = "04A0",
                FirstRow = true,
                LastRow = false,
                FirstColumn = true,
                LastColumn = false,
                NoHorizontalBand = false,
                NoVerticalBand = true
            };
            tableProperties1.Append(tableStyle1);
            tableProperties1.Append(tableWidth1);
            tableProperties1.Append(tableBorders1);
            tableProperties1.Append(tableLook1);
            tbl.Append(tableProperties1);

            if (IsShowHeader)
                tbl.Append(CreateNoHeader(table, Headers));

            Run xRun;
            RunStyle rs = new RunStyle();
            DocDefaults defaults = mainPart.StyleDefinitionsPart.Styles.Descendants<DocDefaults>().FirstOrDefault();
            string fontSize = defaults.RunPropertiesDefault.RunPropertiesBaseStyle.FontSize.Val;
            string fontAscii = defaults.RunPropertiesDefault.RunPropertiesBaseStyle.RunFonts.Ascii.Value;
            RunFonts runFont = defaults.RunPropertiesDefault.RunPropertiesBaseStyle.RunFonts;

            for (int rowIndex = 0; rowIndex <= table.Rows.Count - 1; rowIndex++)
            {
                for (int cellIndex = 0; cellIndex <= table.Columns.Count - 1; cellIndex++)
                {
                    xRun = new Run();
                    cellText.Add(Convert.ToString(table.Rows[rowIndex][cellIndex]));
                }

                TableRow tr = CreateRow(cellText);
                tbl.AppendChild<TableRow>(tr);
                cellText = new ArrayList();
            }

            OpenXmlElement parent = sdt.Parent;
            parent.InsertAfter(tbl, sdt);
            sdt.Remove();
        }

        public static TableRow CreateNoHeader(System.Data.DataTable table, string[] headerResource)
        {
            return CreateNoHeader(table, headerResource, DefaultFontSize);
        }

        public static TableRow CreateNoHeader(System.Data.DataTable table, string[] headerResource, int fontSize)
        {
            TableRow tableHeader = new TableRow();

            for (int i = 0; i <= table.Columns.Count - 1; i++)
            {
                DataColumn column = table.Columns[i];

                if (headerResource[i] != null)
                {
                    Run nRun = new Run(new Text(headerResource[i]));
                    nRun.RunProperties = DefaultRunProperties(fontSize, true);
                    TableCell tc = new TableCell();
                    tc.AppendChild(new Paragraph(nRun));
                    tableHeader.AppendChild(tc);
                    TableRowProperties trp = new TableRowProperties();
                }
            }

            return tableHeader;
        }

        public static TableRow CreateHeader(System.Data.DataTable table, string headerResource)
        {
            TableRow tableHeader = new TableRow();

            foreach (DataColumn column in table.Columns)
            {
                TableCell tc = new TableCell();
                tc.AppendChild(new Paragraph(new Run(new Text(headerResource))));
                tableHeader.AppendChild(tc);
            }

            return tableHeader;
        }

        public static TableRow CreateRow(ArrayList cellText)
        {
            return CreateRow(cellText, DefaultFontSize);
        }

        public static TableRow CreateRow(ArrayList cellText, bool withParagraphProperty)
        {
            if (withParagraphProperty)
                return CreateRow(cellText, DefaultFontSize, true);
            else
                return CreateRow(cellText, DefaultFontSize, false);
        }

        public static TableRow CreateRow(ArrayList cellText, int fontSize)
        {
            return CreateRow(cellText, fontSize, false);
        }

        public static TableRow CreateRow(ArrayList cellText, int fontSize, bool withParagraphProperty)
        {
            TableRow tr = new TableRow();

            foreach (string s in cellText)
            {
                TableCell tc = new TableCell();
                Paragraph p = new Paragraph();
                if (withParagraphProperty)
                    p.ParagraphProperties = DefaultParagraphProperties(true);
                Run r = new Run();
                r.RunProperties = DefaultRunProperties(fontSize);
                Text t = new Text(s);
                r.AppendChild(t);
                p.AppendChild(r);
                tc.AppendChild(p);
                tr.AppendChild(tc);
            }

            return tr;
        }

        public static SdtElement WDGetContentControl(MainDocumentPart mainPart, string contentControlAlias)
        {
            SdtElement control = null/* TODO Change to default(_) if this is not a reference type */;

            foreach (SdtElement sdt in mainPart.Document.Descendants<SdtElement>())
            {
                var alias = sdt.Descendants<SdtAlias>().FirstOrDefault();

                if ((alias != null) && (alias.Val != null) && (alias.Val.HasValue) && (alias.Val.Value == contentControlAlias))
                {
                    control = sdt;
                    break;
                }
            }

            return control;
        }
    }
}
