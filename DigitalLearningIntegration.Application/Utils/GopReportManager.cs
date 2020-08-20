using System;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Serilog;
using System.IO;
using System.Collections.Generic;
using DigitalLearningIntegration.Application.GobEntity.Dto;

namespace DigitalLearningIntegration.Application.Utils
{
    public static class GopReportManager
    {
       /* public static int WriteExcelToSftp(string sftpPath, GopEntity[] dataToExport)
        {
            try
            {
                if (string.IsNullOrEmpty(sftpPath))
                    throw new ArgumentNullException(nameof(sftpPath));

                if (dataToExport == null)
                {
                    throw new ArgumentNullException(nameof(dataToExport));
                }

                var countries = ReadExcelToCountries(@Environment.CurrentDirectory + "\\AppData\\PaisNacionalidadXLSX.xlsx");

                var companies = ReadExcelToCompanies(@Environment.CurrentDirectory + "\\AppData\\SociedadXLSX.xlsx");

                Log.Debug("Creating Excel whit entities.");

                using (var document = SpreadsheetDocument.Create(sftpPath, SpreadsheetDocumentType.Workbook, true))
                {
                    // Add a WorkbookPart to the document.
                    var workbookPart = document.AddWorkbookPart();
                    workbookPart.Workbook = new Workbook();

                    // Add a WorksheetPart to the WorkbookPart.
                    var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet();


                    var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                    var sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Hoja_1" };
                    sheets.Append(sheet);
                    workbookPart.Workbook.Save();
                }

                using (var document = SpreadsheetDocument.Open(sftpPath, true))
                {
                    Log.Debug("Writing in Excel.");

                    var worksheetPart = document.WorkbookPart.WorksheetParts.First();
                    var sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                    var row = new Row();
                    var cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Username"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Password"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "RUT"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Nombre"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "ApellidoPaterno"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "ApellidoMaterno"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "FechaNacimiento"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "EmailPersonal"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Genero"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "EstadoCivil"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "PaisNacionalidad"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "GrupoSanguineo"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Altura"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Peso"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "TallaPantalon"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "TallaCamisa"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "TallaZapatos"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Isapre"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Afp"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "NumeroLicenciaConducir"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "NumeroPasaporte"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "DireccionParticular"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "NumeroDireccionParticular"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "NombreContactoEmergencia"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "FonoContactoEmergencia"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "EmailLaboral"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "FonoLaboral"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "UnidadOrganizacional"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "UnidadNegocio"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Ubicacion"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Cargo"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "EscolaridadSence"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "NivelOcupacional"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "FranquiciaSence"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "TipoContrato"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "FechaInicioContrato"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "FechaTerminoContrato"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "JefaturaDirecta"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "SociedadContratante"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "CentroCosto"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "TelefonoMovil"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "SituacionMilitar"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "MovilidadGeografica"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "FonoFijoEmergencia"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "TelefonoFijo"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Celular"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Instructor"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "FamiliaCargo"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Area"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "ReglaPlanHorario"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "JornadaLaboral"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "CuentaReparto"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Sindicalizado"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Pensionado"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "Discapacitado"
                        }
                    };
                    row.Append(cell);
                    cell = new Cell
                    {
                        DataType = CellValues.String,
                        CellValue = new CellValue()
                        {
                            Text = "CodigoLocal"
                        }
                    };
                    row.Append(cell);

                    sheetData.AppendChild(row);

                    var emptyCell = string.Empty;
                    var dateFormat = "dd-MM-yyyy";

                    foreach (var item in dataToExport)
                    {
                        row = new Row();
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.RutWhitOutDots
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.RutWhitOutDots
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.RutWhitOutDots
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.First_name
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.Surname
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.Second_surname
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.Dbirthday.HasValue ? item.Dbirthday.Value.ToString(dateFormat) : string.Empty
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.GenderToDl.ToString()
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CivilStatusToDl
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = !string.IsNullOrEmpty(item.Country_code) && countries.Keys.Any(k => k == item.Country_code) ? countries[item.Country_code].CountryNameDL : string.Empty
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                //Text = item.Health_company
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                //Text = item.Pension_fund
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.Address
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = !string.IsNullOrEmpty(item.Email) && IsValidEmail(item.Email.Trim().ToLower()) ? item.Email.Trim().ToLower() : string.Empty
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.Office_phone
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CurrentJob.CustomAttributes.Gerencia
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CurrentJob.CustomAttributes.AreaGastos
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.District
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CurrentJob.Role.Name
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CurrentJob.ContractTypeToDl
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CurrentJob.DstartDate.HasValue ? item.CurrentJob.DstartDate.Value.ToString(dateFormat) : string.Empty
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CurrentJob.DendDate.HasValue ? item.CurrentJob.DendDate.Value.ToString(dateFormat) : string.Empty
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CurrentJob != null && item.CurrentJob.Boss != null && !string.IsNullOrEmpty(item.CurrentJob.Boss.Rut) ? item.CurrentJob.Boss.Rut.Replace(".", string.Empty) : string.Empty
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = companies.Keys.Any(k => k == item.CurrentJob.CompanyId) ? companies[item.CurrentJob.CompanyId].CompanyRut.Replace(".", string.Empty) : string.Empty
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CurrentJob.CostCenter
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.Phone
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = emptyCell
                            }
                        };
                        row.Append(cell);
                        cell = new Cell
                        {
                            DataType = CellValues.String,
                            CellValue = new CellValue()
                            {
                                Text = item.CurrentJob != null && item.CurrentJob.CustomAttributes != null && !string.IsNullOrEmpty(item.CurrentJob.CustomAttributes.CodigoLocal) ? item.CurrentJob.CustomAttributes.CodigoLocal : emptyCell
                            }
                        };
                        row.Append(cell);

                        sheetData.AppendChild(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }

            Log.Debug("Excel was create correctly.");

            return dataToExport.Length;
        }   */     

        public static void GenerateLogMonitorFile(string filePath, LogInfo info)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                    throw new ArgumentNullException(nameof(filePath));

                if (info == null)
                    throw new ArgumentNullException(nameof(info));

                Log.Debug("Creating monitor txt whit logs.");

                var ostrm = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                var writer = new StreamWriter(ostrm);

                writer.WriteLine("- Id de Proceso: " + info.ProcessId);
                writer.WriteLine("- Nombre de Archivo: " + info.NameOfFile);
                writer.WriteLine("- Fecha de Procesamiento: " + info.DateOfFile.ToString("HH:mm yyyyMMdd"));
                writer.WriteLine("- Estado: " + info.State);
                writer.WriteLine("- Total de registros procesados: " + info.CountOfRows);
                writer.Close();
                ostrm.Close();
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }

            Log.Debug("Log monitor file was create correctly.");
        }        

        static bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    throw new ArgumentNullException(nameof(email));

                return RegexUtilities.IsValidEmail(email);
                //var addr = new System.Net.Mail.MailAddress(email);
                //return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
