using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NTCKamaz.Data.Clasees;
using NTCKamaz.Data.Classes;
using NTCKamaz.Data.Context;
using NTCKamaz.Data.ViewClasses;
using NTCKamaz.Models;
using System.Diagnostics;

namespace NTCKamaz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationContext _applicationContext;

        private readonly Model model;

        public HomeController(ILogger<HomeController> logger, ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;

            model = new Model
            {
                PCViews = new List<PCView>(),
                UserViews = new List<UserView>(),
                DepartmentViews = new List<DepartmentView>(),
                ComponentViews = new List<ComponentView>(),
                MonitorViews = new List<MonitorView>(),
                OSViews = new List<OSView>(),
                AssemblyViews = new List<AssemblyView>(),
                MOLViews = new List<MOLView>(),
                InventoryNumberViews = new List<InventoryNumberView>(),
                AdditionalDevicesViews = new List<AdditionalDevicesView>(),
                PrinterViews = new List<PrinterView>(),
            };
        }

        public IActionResult Index()
        {
            return RedirectToAction("Main");
        }


        [HttpGet]
        public IActionResult Computers(string selectedOption, string search)
        {
            try
            {
                var computers = _applicationContext.Computers.ToList();

                if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
                {
                    search = search.Trim();

                    switch (selectedOption)
                    {
                        case "ID":
                            computers = computers.Where(c => c.ID != null && c.ID.ToString().Trim() == search.Trim()).ToList();
                            break;
                        case "Имя ПК":
                            computers = computers.Where(c =>
                                c.pcName != null &&
                                c.pcName.Trim().ToLower().Contains(search.ToLower())).ToList();
                            break;
                        case "Дата выдачи":
                            computers = computers.Where(c =>
                                c.issueDate != null &&
                                c.issueDate.Trim().ToLower().Contains(search.ToLower())).ToList();
                            break;
                        case "Пользователь":
                            computers = computers.Where(c =>
                                c.userID != null &&
                                _applicationContext.Users.FirstOrDefault(u => u.ID == c.userID)?.UserName != null &&
                                _applicationContext.Users.FirstOrDefault(u => u.ID == c.userID)?.UserName?.ToLower().Trim().Contains(search.ToLower().Trim()) == true
                            ).ToList();
                            break;
                        case "Отдел":
                            computers = computers.Where(c =>
                                c.departmentID != null &&
                                _applicationContext.Departments.FirstOrDefault(d => d.ID == c.departmentID)?.ShortName != null &&
                                _applicationContext.Departments.FirstOrDefault(d => d.ID == c.departmentID)?.ShortName?.ToLower().Trim().Contains(search.ToLower().Trim()) == true
                            ).ToList();
                            break;
                        case "Марка":
                            computers = computers.Where(c =>
                                c.markID != null &&
                                _applicationContext.PCMarks.FirstOrDefault(m => m.ID == c.markID)?.pcMark != null &&
                                _applicationContext.PCMarks.FirstOrDefault(m => m.ID == c.markID).pcMark.ToLower().Trim().Contains(search.ToLower())).ToList();
                            break;

                        case "SN системного блока":
                            computers = computers.Where(c =>
                                c.snSystemBlock != null &&
                                c.snSystemBlock.Trim().ToLower().Contains(search.ToLower())).ToList();
                            break;
                        case "Характеристика":
                            computers = computers.Where(c =>
                                c.assemblyID != null &&
                                GetPCCharacteristic(c.assemblyID.Value).ToLower().Contains(search.ToLower())
                            ).ToList();
                            break;

                        case "Поставщик":
                            computers = computers.Where(c =>
                                c.providerID != null &&
                                _applicationContext.PСProviders.FirstOrDefault(p => p.ID == c.providerID)?.providerName != null &&
                                _applicationContext.PСProviders.FirstOrDefault(p => p.ID == c.providerID)?.providerName?.ToLower().Trim() == search.ToLower()).ToList();
                            break;

                        case "Категория":
                            computers = computers.Where(c =>
                                c.categoryID != null &&
                                _applicationContext.PCCategories.FirstOrDefault(cat => cat.ID == c.categoryID)?.pcCategory != null &&
                                _applicationContext.PCCategories.FirstOrDefault(cat => cat.ID == c.categoryID)?.pcCategory?.ToLower().Trim() == search.ToLower()).ToList();
                            break;

                        case "ОС":
                            computers = computers.Where(c =>
                                c.ocID != null &&
                                _applicationContext.OC.FirstOrDefault(os => os.ID == c.ocID)?.OCName != null &&
                                _applicationContext.OC.FirstOrDefault(os => os.ID == c.ocID)?.OCName?.ToLower().Trim().Contains(search.ToLower()) == true
                            ).ToList();
                            break;

                        case "Инвентарный номер":
                            computers = computers.Where(c =>
                                c.inventoryNumberID != null &&
                                _applicationContext.InventoryNumbers.FirstOrDefault(inv => inv.ID == c.inventoryNumberID)?.inventoryNumber != null &&
                                _applicationContext.InventoryNumbers.FirstOrDefault(inv => inv.ID == c.inventoryNumberID)?.inventoryNumber?.ToLower().Trim().Contains(search.ToLower()) == true
                            ).ToList();
                            break;

                        case "Скл №ПК":
                            computers = computers.Where(c =>
                                c.skladtNumber != null &&
                                c.skladtNumber.Trim().ToLower().Contains(search.ToLower())
                            ).ToList();
                            break;

                        case "Монитор":
                            computers = computers.Where(c =>
                                c.monitorID != null &&
                                GetMonitorMark(c.monitorID)?.ToLower().Contains(search.ToLower()) == true
                            ).ToList();
                            break;

                        case "SN монитора":
                            computers = computers.Where(c =>
                                c.monitorSN != null &&
                                c.monitorSN.Trim().ToLower().Contains(search.ToLower())
                            ).ToList();
                            break;

                        case "Переферийные устройства":
                            computers = computers.Where(c =>
                                c.additionallDeviceID != null &&
                                GetAdditionalDevicesNames(c.additionallDeviceID.Value)?.ToLower().Contains(search.ToLower()) == true
                            ).ToList();
                            break;

                        case "Кабинет":
                            computers = computers.Where(c =>
                                c.cabinet != null &&
                                c.cabinet.Trim().ToLower().Contains(search.ToLower())
                            ).ToList();
                            break;

                        case "МОЛ":
                            computers = computers.Where(c =>
                                c.molID != null &&
                                _applicationContext.MOLs.FirstOrDefault(mol => mol.ID == c.molID)?.userName != null &&
                                _applicationContext.MOLs.FirstOrDefault(mol => mol.ID == c.molID)?.userName.ToLower().Trim().Contains(search.ToLower()) == true
                            ).ToList();
                            break;

                        case "Проект":
                            computers = computers.Where(c =>
                                c.projectID != null &&
                                _applicationContext.Projects.FirstOrDefault(p => p.ID == c.projectID)?.project != null &&
                                _applicationContext.Projects.FirstOrDefault(p => p.ID == c.projectID)?.project.ToLower().Trim().Contains(search.ToLower()) == true
                            ).ToList();
                            break;

                        case "Основание":
                            computers = computers.Where(c =>
                                c.foundation != null &&
                                c.foundation.Trim().ToLower().Contains(search.ToLower())
                            ).ToList();
                            break;


                        case "Примечание":
                            computers = computers.Where(c =>
                                c.note != null &&
                                c.note.Trim().ToLower().Contains(search.ToLower())
                            ).ToList();
                            break;

                        default:
                            break;
                    }
                }



                var pcViews = computers.Select(pc => new PCView
                {
                    ID = pc.ID,
                    pcName = pc.pcName,
                    dateOfIssue = pc.issueDate,
                    userName = _applicationContext.Users.Find(pc.userID)?.UserName?.Trim(),
                    departmentName = _applicationContext.Departments.Find(pc.departmentID)?.ShortName?.Trim(),
                    pcMark = _applicationContext.PCMarks.Find(pc.markID)?.pcMark?.Trim(),
                    snSystemBlock = pc.snSystemBlock,
                    characteristic = GetPCCharacteristic(pc),
                    providerName = _applicationContext.PСProviders.Find(pc.providerID)?.providerName,
                    category = _applicationContext.PCCategories.Find(pc.categoryID)?.pcCategory,
                    OS = _applicationContext.OC.Find(pc.ocID)?.OCName,
                    inventoryNumber = _applicationContext.InventoryNumbers.Find(pc.inventoryNumberID)?.inventoryNumber,
                    skladNumberPC = pc.skladtNumber,
                    monitorName = GetMonitorMark(pc.monitorID),
                    snMonitor = _applicationContext.Monitors.Find(pc.monitorID)?.monitorSN?.Trim(),
                    additionalDevices = GetAdditionalDevices(pc.additionallDeviceID),
                    cabinet = _applicationContext.Users.Find(pc.userID)?.Cabinet?.Trim(),
                    MOL = _applicationContext.MOLs.Find(pc.molID)?.userName,
                    projectName = _applicationContext.Projects.Find(pc.projectID)?.project,
                    foundation = pc.foundation,
                    note = pc.note
                });

                model.PCViews = pcViews.ToList();

                return View(model);
            }

            catch (Exception ex)
            {
                throw;
            }

        }




        private string GetPCCharacteristic(PC pc)
        {

            var assembly = _applicationContext.Assemblies.Find(pc.assemblyID);
            if (assembly == null)
                return string.Empty;

            var processorName = _applicationContext.Processors.FirstOrDefault(p => p.ID == assembly.processorID)?.processorName?.Trim();
            var motherboardName = _applicationContext.Motherboards.FirstOrDefault(p => p.ID == assembly.motherboardID)?.motherboardName?.Trim();
            var ramName = _applicationContext.RAMS.FirstOrDefault(r => r.ID == assembly.ramID)?.RAM?.Trim();
            var gpuName = _applicationContext.Videocards.FirstOrDefault(g => g.ID == assembly.graphicsCardID)?.graphicsCard?.Trim();
            var hddName = _applicationContext.HDDs.FirstOrDefault(h => h.ID == assembly.hddID)?.HDD?.Trim();
            var ssdName = _applicationContext.SSDs.FirstOrDefault(s => s.ID == assembly.ssdID)?.SSD?.Trim();
            var psuName = _applicationContext.PowerSupplies.FirstOrDefault(p => p.ID == assembly.bpID)?.bpName?.Trim();

            var characteristicParts = new List<string>
        {
            processorName, motherboardName, ramName, gpuName, hddName, ssdName, psuName
        };

            return string.Join("/", characteristicParts.Where(part => !string.IsNullOrEmpty(part)));

        }



        private string GetPCCharacteristic(int assemblyID)
        {

            var assembly = _applicationContext.Assemblies.FirstOrDefault(a => a.ID == assemblyID);
            if (assembly == null)
                return string.Empty;

            var processorID = assembly.processorID;
            var motherboardID = assembly.motherboardID;
            var ramID = assembly.ramID;
            var graphicsCardID = assembly.graphicsCardID;
            var hddID = assembly.hddID;
            var ssdID = assembly.ssdID;
            var bpID = assembly.bpID;

            var processorName = _applicationContext.Processors.FirstOrDefault(p => p.ID == processorID)?.processorName;
            var motherboardName = _applicationContext.Motherboards.FirstOrDefault(m => m.ID == motherboardID)?.motherboardName;
            var ramName = _applicationContext.RAMS.FirstOrDefault(r => r.ID == ramID)?.RAM;
            var graphicsCardName = _applicationContext.Videocards.FirstOrDefault(v => v.ID == graphicsCardID)?.graphicsCard;
            var hddName = _applicationContext.HDDs.FirstOrDefault(h => h.ID == hddID)?.HDD;
            var ssdName = _applicationContext.SSDs.FirstOrDefault(s => s.ID == ssdID)?.SSD;
            var bpName = _applicationContext.PowerSupplies.FirstOrDefault(b => b.ID == bpID)?.bpName;

            return $"{processorName} / {motherboardName} / {ramName} / {graphicsCardName} / {hddName} / {ssdName} / {bpName}";

        }




        private string GetMonitorMark(int? monitorID)
        {
            try
            {
                if (monitorID.HasValue)
                {
                    var monitor = _applicationContext.Monitors.FirstOrDefault(m => m.ID == monitorID);
                    if (monitor != null && monitor.markID.HasValue)
                    {
                        var markID = monitor.markID;
                        var monitorMark = _applicationContext.MonitorMarks.FirstOrDefault(mm => mm.ID == markID)?.markMonitor;
                        return monitorMark;
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                // Обработка исключения, можно здесь добавить логирование ошибки
                // и вернуть пустую строку, чтобы не прерывать работу приложения
                return string.Empty;
            }
        }


        private string GetAdditionalDevices(int? additionalDeviceID)
        {
            try
            {
                if (additionalDeviceID.HasValue)
                {
                    var additionalDevices = _applicationContext.AdditionalDevices.FirstOrDefault(ad => ad.ID == additionalDeviceID);
                    if (additionalDevices != null)
                    {
                        var keyboardAndMouse = _applicationContext.KeyboardsAndMouses.FirstOrDefault(km => km.ID == additionalDevices.keyboardMouseID)?.keyboardMouse;
                        var webCam = _applicationContext.Webcams.FirstOrDefault(w => w.ID == additionalDevices.webcamID)?.webcam;
                        var headphone = _applicationContext.Headphones.FirstOrDefault(h => h.ID == additionalDevices.headphonesID)?.headphones;
                        var networkFilter = _applicationContext.NetworkFilters.FirstOrDefault(n => n.ID == additionalDevices.networkFilterID)?.networkFilter;

                        var additionalDevicesParts = new List<string> { keyboardAndMouse, webCam, headphone, networkFilter };
                        return string.Join("/", additionalDevicesParts.Where(part => !string.IsNullOrEmpty(part)));
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                // Обработка исключения, можно здесь добавить логирование ошибки
                // и вернуть пустую строку, чтобы не прерывать работу приложения
                return string.Empty;
            }
        }




        private string GetAdditionalDevicesNames(int additionalDeviceID)
        {
            var additionalDevicesNames = new List<string>();
            var additionalDevice = _applicationContext.AdditionalDevices.FirstOrDefault(ad => ad.ID == additionalDeviceID);

            if (additionalDevice != null)
            {
                if (additionalDevice.keyboardMouseID != null)
                {
                    var keyboardMouseName = _applicationContext.KeyboardsAndMouses.FirstOrDefault(k => k.ID == additionalDevice.keyboardMouseID)?.keyboardMouse;
                    if (!string.IsNullOrEmpty(keyboardMouseName))
                        additionalDevicesNames.Add(keyboardMouseName);
                }

                if (additionalDevice.webcamID != null)
                {
                    var webcamName = _applicationContext.Webcams.FirstOrDefault(w => w.ID == additionalDevice.webcamID)?.webcam;
                    if (!string.IsNullOrEmpty(webcamName))
                        additionalDevicesNames.Add(webcamName);
                }

                if (additionalDevice.headphonesID != null)
                {
                    var headphonesName = _applicationContext.Headphones.FirstOrDefault(h => h.ID == additionalDevice.headphonesID)?.headphones;
                    if (!string.IsNullOrEmpty(headphonesName))
                        additionalDevicesNames.Add(headphonesName);
                }

                if (additionalDevice.networkFilterID != null)
                {
                    var networkFilterName = _applicationContext.NetworkFilters.FirstOrDefault(nf => nf.ID == additionalDevice.networkFilterID)?.networkFilter;
                    if (!string.IsNullOrEmpty(networkFilterName))
                        additionalDevicesNames.Add(networkFilterName);
                }

                if (additionalDevice.pcID != null)
                {
                    var pcName = _applicationContext.Computers.FirstOrDefault(pc => pc.ID == additionalDevice.pcID)?.pcName;
                    if (!string.IsNullOrEmpty(pcName))
                        additionalDevicesNames.Add(pcName);
                }
            }

            return string.Join(" / ", additionalDevicesNames);
        }



        [HttpGet]
        public IActionResult AddPC()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPC([FromForm] string? PCName, string? dateOfIssue, string? userName, string? departmentName, string? pcMark, string? snSystemOfBlock, string? characteristic, string? providerName, string? category, string? OS, string? inventoryNumber, string? skladNumberPC, string? monitorName, string? snMonitor, string? additionalDevices, string? cabinet, string? MOL, string? projectName, string? foundation, string? note)
        {


            string processor = "";
            string motherboard = "";
            string ram = "";
            string graphicsCard = "";
            string hdd = "";
            string ssd = "";
            string bpName = "";



            if (string.IsNullOrEmpty(characteristic))
            {

            }

            else
            {
                string[] partsOfPC = characteristic.Split('/');


                for (int i = 0; i < partsOfPC.Length; i++)
                {
                    string part = partsOfPC[i].Trim();

                    switch (i)
                    {
                        case 0:
                            processor = part;
                            break;
                        case 1:
                            motherboard = part;
                            break;
                        case 2:
                            ram = part;
                            break;
                        case 3:
                            graphicsCard = part;
                            break;
                        case 4:
                            hdd = part;
                            break;
                        case 5:
                            ssd = part;
                            break;
                        case 6:
                            bpName = part;
                            break;
                    }
                }
            }


            var pcProcessor = GetOrCreatePCProcessor(processor);
            var PCProcessorID = pcProcessor.ID;

            var pcSSD = GetOrCreatePCSSD(ssd);
            var PCSSDID = pcSSD.ID;


            var pcMotherboard = GetOrCreatePCMotherboard(motherboard);
            var pcMotherboardID = pcMotherboard.ID;

            var pcVideocard = GetOrCreatePCVideocard(graphicsCard);
            var PCVideocardID = pcVideocard.ID;


            var pcRam = GetOrCreatePCRam(ram);
            var pcRAMID = pcRam.ID;


            var pcHDD = GetOrCreatePCHDD(hdd);
            var pcHDDID = pcHDD.ID;

            var pcPowerSupply = GetOrCreatePowerSupply(bpName);
            var pcBPID = pcPowerSupply.ID;



            var assemblyPC = CreatePCAssembly(PCProcessorID, pcMotherboardID, pcRAMID, PCVideocardID, pcHDDID, PCSSDID, pcBPID);
            var assemblyPCID = assemblyPC.ID;

            var pcComponent = CreatePCComponent(PCProcessorID, pcMotherboardID, pcRAMID, PCVideocardID, pcHDDID, PCSSDID, pcBPID);


            var monitorMark = GetOrCreateMonitorMark(monitorName);
            var MarkMonitorID = monitorMark.ID;

            var InventoryNumber = GetOrCreateInventoryNumber(inventoryNumber);
            var InventoryNumberID = InventoryNumber.ID;



            var os = GetOrCreateOS(OS);
            var osID = os?.ID;


            var pcCategory = GetOrCreatePCCategory(category);
            var PCCategoryID = pcCategory.ID;

            var pcProvider = GetOrCreatePCProvider(providerName);
            var PCProviderID = pcProvider.ID;

            var monitor = CreatePCMonitor(MarkMonitorID, snMonitor, InventoryNumberID, PCProviderID);
            var monitorID = monitor.ID;

            var PCMark = GetOrCreatePCMark(pcMark);
            var pcMarkID = PCMark.ID;

            var department = GetOrCreateDepartment(departmentName);
            var departmentID = department.ID;

            var user = CreateUser(userName, departmentID, cabinet);
            var userID = user.ID;


            string keyboardAndMouse = "";
            string WebCam = "";
            string Headphone = "";
            string NetworkFilter = "";


            if (string.IsNullOrEmpty(additionalDevices))
            {

            }
            else
            {
                string[] devicesOfPC = additionalDevices.Split('/');


                for (int i = 0; i < devicesOfPC.Length; i++)
                {
                    string device = devicesOfPC[i].Trim();

                    switch (i)
                    {
                        case 0:
                            keyboardAndMouse = device;
                            break;
                        case 1:
                            WebCam = device;
                            break;
                        case 2:
                            Headphone = device;
                            break;
                        case 3:
                            NetworkFilter = device;
                            break;
                    }
                }
            }


            var keyboardAndMouseEntity = GetOrCreateKeyboardAndMouse(keyboardAndMouse);
            var keyboardAndMouseID = keyboardAndMouseEntity.ID;

            var webCamEntity = GetOrCreateWebcam(WebCam);
            var webCamID = webCamEntity.ID;

            var networkFilterEntity = GetOrCreateNetworkFilter(NetworkFilter);
            var networkFilterID = networkFilterEntity.ID;

            var headphoneEntity = GetOrCreateHeadphone(Headphone);
            var headphoneID = headphoneEntity.ID;


            var additionalDevice = CreateAdditionalDevice(keyboardAndMouseID, webCamID, networkFilterID, headphoneID);
            var AdditionalDeviceID = additionalDevice.ID;

            var mol = CreateMOL(MOL);
            var molID = mol.ID;

            var project = CreateProject(projectName);
            var projectID = project.ID;




            var newPC = new PC
            {
                ID = GenerateNextPCID(),
                pcName = PCName?.Trim(),
                issueDate = dateOfIssue,
                userID = userID,
                departmentID = departmentID,
                markID = pcMarkID,
                snSystemBlock = snSystemOfBlock?.Trim(),
                assemblyID = assemblyPCID,
                providerID = PCProviderID,
                categoryID = PCCategoryID,
                ocID = osID,
                inventoryNumberID = InventoryNumberID,
                skladtNumber = skladNumberPC?.Trim(),
                monitorID = monitorID,
                monitorSN = monitor.monitorSN,
                additionallDeviceID = (int)AdditionalDeviceID,
                cabinet = user.Cabinet,
                molID = molID,
                projectID = projectID,
                foundation = foundation?.Trim(),
                note = note?.Trim(),
            };


            _applicationContext.Computers.Add(newPC);
            _applicationContext.SaveChanges();

            var computerID = newPC.ID;

            // Убедимся, что объекты не равны null, прежде чем присваивать pcID
            if (os != null)
            {
                os.pcID = computerID;
            }

            if (monitor != null)
            {
                monitor.pcID = computerID;
            }

            if (additionalDevice != null)
            {
                additionalDevice.pcID = computerID;
            }

            if (assemblyPC != null)
            {
                assemblyPC.pcID = computerID;
            }

            if (InventoryNumber != null)
            {
                InventoryNumber.pcID = computerID;
                InventoryNumber.monitorID = monitorID;
            }

            if (project != null)
            {
                project.pcID = computerID;
            }

            if (user != null)
            {
                user.pcID = computerID;
            }

            if (mol != null)
            {
                mol.pcID = computerID;
            }


            // Продолжим остальные действия
            _applicationContext.SaveChanges();

            return RedirectToAction("Computers");



        }



        private AdditionalDevice CreateAdditionalDevice(int keyboardMouseID, int webcamID, int networkFilterID, int headphonesID)
        {
            try
            {
                var newAdditionalDevice = new AdditionalDevice
                {
                    keyboardMouseID = keyboardMouseID,
                    webcamID = webcamID,
                    networkFilterID = networkFilterID,
                    headphonesID = headphonesID
                };

                _applicationContext.Add(newAdditionalDevice);
                _applicationContext.SaveChanges();

                return newAdditionalDevice;
            }
            catch (Exception ex)
            {
                // Обработка исключения, можно здесь добавить логирование ошибки
                // и вернуть null или выбросить исключение, в зависимости от требований
                throw new Exception("Ошибка при создании AdditionalDevice.", ex);
            }
        }



        private MOL CreateMOL(string userName)
        {


            var mol = new MOL
            {
                userName = userName?.Trim()
            };

            _applicationContext.Add(mol);
            _applicationContext.SaveChanges();

            return mol;


        }




        private Project CreateProject(string projectName)
        {


            var project = new Project
            {
                project = projectName?.Trim()
            };

            _applicationContext.Add(project);
            _applicationContext.SaveChanges();

            return project;


        }



        private KeyboardsAndMouse GetOrCreateKeyboardAndMouse(string keyboardAndMouse)
        {

            var existingKeyboardAndMouse = _applicationContext.KeyboardsAndMouses.FirstOrDefault(k => k.keyboardMouse == keyboardAndMouse);
            if (existingKeyboardAndMouse != null)
            {
                return existingKeyboardAndMouse;
            }

            var newKeyboardAndMouse = new KeyboardsAndMouse { keyboardMouse = keyboardAndMouse.Trim().ToUpper() };
            _applicationContext.Add(newKeyboardAndMouse);
            _applicationContext.SaveChanges();

            return newKeyboardAndMouse;


        }


        private Webcam GetOrCreateWebcam(string webCam)
        {

            var existingWebcam = _applicationContext.Webcams.FirstOrDefault(w => w.webcam == webCam);
            if (existingWebcam != null)
            {
                return existingWebcam;
            }

            var newWebcam = new Webcam { webcam = webCam.Trim().ToUpper() };
            _applicationContext.Add(newWebcam);
            _applicationContext.SaveChanges();

            return newWebcam;


        }



        private NetworkFilter GetOrCreateNetworkFilter(string networkFilter)
        {

            var trimmedNetworkFilter = networkFilter.Trim().ToUpper();
            var existingNetworkFilter = _applicationContext.NetworkFilters.FirstOrDefault(n => n.networkFilter == trimmedNetworkFilter);

            if (existingNetworkFilter == null)
            {
                try
                {
                    var newNetworkFilter = new NetworkFilter { networkFilter = trimmedNetworkFilter };
                    _applicationContext.Add(newNetworkFilter);
                    _applicationContext.SaveChanges();
                    return newNetworkFilter;
                }
                catch (Exception ex)
                {
                    // Handle the exception here, you can log it, throw a custom exception, etc.
                    throw new ApplicationException("Error occurred while creating a new network filter.", ex);
                }
            }

            return existingNetworkFilter;
        }





        private Headphone GetOrCreateHeadphone(string headphone)
        {

            var trimmedHeadphone = headphone.Trim().ToUpper();
            var existingHeadphone = _applicationContext.Headphones.FirstOrDefault(h => h.headphones == trimmedHeadphone);

            if (existingHeadphone == null)
            {
                try
                {
                    var newHeadphone = new Headphone { headphones = trimmedHeadphone };
                    _applicationContext.Add(newHeadphone);
                    _applicationContext.SaveChanges();
                    return newHeadphone;
                }
                catch (Exception ex)
                {
                    // Handle the exception here, you can log it, throw a custom exception, etc.
                    throw new ApplicationException("Error occurred while creating a new headphone.", ex);
                }
            }

            return existingHeadphone;
        }

        private PCProcessor GetOrCreatePCProcessor(string processor)
        {
            var trimmedProcessor = processor.ToUpper().Trim();
            var existingPCProcessor = _applicationContext.Processors.FirstOrDefault(p => p.processorName == trimmedProcessor);

            if (existingPCProcessor == null)
            {
                try
                {
                    var newPCProcessor = new PCProcessor { processorName = trimmedProcessor };
                    _applicationContext.Add(newPCProcessor);
                    _applicationContext.SaveChanges();
                    return newPCProcessor;
                }
                catch (Exception ex)
                {
                    // Handle the exception here, you can log it, throw a custom exception, etc.
                    throw new ApplicationException("Error occurred while creating a new PC processor.", ex);
                }
            }

            return existingPCProcessor;
        }



        private PCAssembly CreatePCAssembly(int? processorID, int? motherboardID, int? ramID, int? graphicsCardID, int? hddID, int? ssdID, int? bpID)
        {
            var newAssemblyPC = new PCAssembly
            {
                processorID = processorID,
                motherboardID = motherboardID,
                ramID = ramID,
                graphicsCardID = graphicsCardID,
                hddID = hddID,
                ssdID = ssdID,
                bpID = bpID
            };

            try
            {
                _applicationContext.Add(newAssemblyPC);
                _applicationContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it, throw a custom exception, etc.
                throw new ApplicationException("Error occurred while creating a new PC assembly.", ex);
            }

            return newAssemblyPC;
        }



        private PCComponent CreatePCComponent(int? processorID, int? motherboardID, int? ramID, int? graphicsCardID, int? hddID, int? ssdID, int? bpID)
        {
            var newPCComponent = new PCComponent
            {
                processorID = processorID,
                motherboardID = motherboardID,
                ramID = ramID,
                graphicsCardID = graphicsCardID,
                hddID = hddID,
                ssdID = ssdID,
                bpID = bpID
            };

            try
            {
                _applicationContext.Add(newPCComponent);
                _applicationContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it, throw a custom exception, etc.
                throw new ApplicationException("Error occurred while creating a new PC component.", ex);
            }

            return newPCComponent;
        }



        private PCMonitor CreatePCMonitor(int markID, string monitorSN, int inventoryNumberID, int providerID)
        {
            var newMonitor = new PCMonitor
            {
                markID = markID,
                monitorSN = monitorSN,
                inventoryNumberID = inventoryNumberID,
                providerID = providerID
            };

            try
            {
                _applicationContext.Add(newMonitor);
                _applicationContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it, throw a custom exception, etc.
                throw new ApplicationException("Error occurred while creating a new PC monitor.", ex);
            }

            return newMonitor;
        }


        private PCMark GetOrCreatePCMark(string pcMark)
        {
            var trimmedPCMark = pcMark?.Trim().ToUpper(); // Safe null-check

            // If pcMark is not null or empty, look for an existing PCMark with the given value.
            if (!string.IsNullOrEmpty(trimmedPCMark))
            {
                var existingPCMark = _applicationContext.PCMarks.FirstOrDefault(m => m.pcMark == trimmedPCMark);

                if (existingPCMark != null)
                {
                    return existingPCMark;
                }
            }
            else
            {
                // If pcMark is null or empty, check if there's already a PCMark with a null value.
                var existingNullPCMark = _applicationContext.PCMarks.FirstOrDefault(m => m.pcMark == null);

                if (existingNullPCMark != null)
                {
                    return existingNullPCMark;
                }
            }

            // If no existing PCMark found, create a new one and add it to the database.
            var newPCMark = new PCMark { pcMark = trimmedPCMark };
            _applicationContext.Add(newPCMark);
            _applicationContext.SaveChanges();
            return newPCMark;
        }


        private Department GetOrCreateDepartment(string departmentName)
        {
            var trimmedDepartmentName = departmentName?.Trim().ToUpper(); // Безопасная проверка на null

            if (!string.IsNullOrEmpty(trimmedDepartmentName))
            {
                // Если departmentName не равен null или пустому значению, ищем существующий отдел с заданным ShortName.
                var existingDepartment = _applicationContext.Departments.FirstOrDefault(d => d.ShortName == trimmedDepartmentName);

                if (existingDepartment != null)
                {
                    return existingDepartment;
                }
            }
            else
            {
                // Если departmentName равен null или пустому значению, проверяем, есть ли уже отдел с пустым ShortName.
                var existingNullDepartment = _applicationContext.Departments.FirstOrDefault(d => d.ShortName == null);

                if (existingNullDepartment != null)
                {
                    return existingNullDepartment;
                }
            }

            // Если существующий отдел не найден, создаем новый и добавляем его в базу данных.
            var newDepartment = new Department { ShortName = trimmedDepartmentName };
            _applicationContext.Add(newDepartment);
            _applicationContext.SaveChanges();
            return newDepartment;
        }



        private User CreateUser(string userName, int departmentID, string cabinet)
        {

            var newUser = new User
            {
                UserName = userName?.Trim(),
                IDDepartment = departmentID,
                Cabinet = cabinet
            };

            try
            {
                _applicationContext.Add(newUser);
                _applicationContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Handle the exception here, you can log it, throw a custom exception, etc.
                throw new ApplicationException("Error occurred while creating a new user.", ex);
            }

            return newUser;
        }





        private MonitorMark GetOrCreateMonitorMark(string markMonitor)
        {
            try
            {
                var trimmedMarkMonitor = markMonitor?.Trim().ToUpper();

                var existingMarkMonitor = _applicationContext.MonitorMarks.FirstOrDefault(i => i.markMonitor == trimmedMarkMonitor);

                if (existingMarkMonitor == null)
                {
                    var newMarkMonitor = new MonitorMark { markMonitor = trimmedMarkMonitor };

                    _applicationContext.MonitorMarks.Add(newMarkMonitor);
                    _applicationContext.SaveChanges();

                    return newMarkMonitor;
                }

                return existingMarkMonitor;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при выполнении операции GetOrCreateMonitorMark: " + ex.Message, ex);
            }
        }

        private InventoryNumber GetOrCreateInventoryNumber(string inventoryNumber)
        {
            var trimmedInventoryNumber = inventoryNumber?.Trim().ToUpper(); // Безопасная проверка на null


            var newInventoryNumber = new InventoryNumber { inventoryNumber = trimmedInventoryNumber };
            _applicationContext.Add(newInventoryNumber);
            _applicationContext.SaveChanges();
            return newInventoryNumber;




        }




        private OS? GetOrCreateOS(string osName)
        {
            if (osName.IsNullOrEmpty())
            {
                return null;
            }
            else
            {
                var trimmedOsName = osName.Trim().ToUpper();
                var existingOS = _applicationContext.OC.FirstOrDefault(os => os.OCName == trimmedOsName);

                if (existingOS == null)
                {
                    var newOS = new OS { OCName = trimmedOsName };
                    _applicationContext.OC.Add(newOS);
                    _applicationContext.SaveChanges();
                    return newOS;
                }

                return existingOS;
            }
        }

        private PCCategory GetOrCreatePCCategory(string pcCategory)
        {
            try
            {
                var existingPCCategory = _applicationContext.PCCategories.FirstOrDefault(c => c.pcCategory == pcCategory);
                if (existingPCCategory == null)
                {
                    // Создаем и сохраняем новую сущность PCCategory
                    var newPCCategory = new PCCategory { pcCategory = pcCategory?.Trim().ToUpper() };
                    _applicationContext.Add(newPCCategory);
                    _applicationContext.SaveChanges();
                    return newPCCategory;
                }

                return existingPCCategory;
            }
            catch (Exception ex)
            {
                // Обработка исключения, логирование ошибки и т.д.
                throw new ApplicationException("Error occurred while getting or creating PCCategory.", ex);
            }
        }

        private PCProvider GetOrCreatePCProvider(string providerName)
        {
            try
            {
                var existingPCProvider = _applicationContext.PСProviders.FirstOrDefault(p => p.providerName == providerName);
                if (existingPCProvider == null)
                {
                    // Создаем и сохраняем новую сущность PCProvider
                    var newPCProvider = new PCProvider { providerName = providerName?.Trim().ToUpper() };
                    _applicationContext.Add(newPCProvider);
                    _applicationContext.SaveChanges();
                    return newPCProvider;
                }

                return existingPCProvider;
            }
            catch (Exception ex)
            {
                // Обработка исключения, логирование ошибки и т.д.
                throw new ApplicationException("Error occurred while getting or creating PCProvider.", ex);
            }
        }

        private PCHDD GetOrCreatePCHDD(string hdd)
        {
            try
            {
                var existingPCHDD = _applicationContext.HDDs.FirstOrDefault(h => h.HDD == hdd);
                if (existingPCHDD == null)
                {
                    // Создаем и сохраняем новую сущность PCHDD
                    var newPCHDD = new PCHDD { HDD = hdd?.Trim().ToUpper() };
                    _applicationContext.Add(newPCHDD);
                    _applicationContext.SaveChanges();
                    return newPCHDD;
                }

                return existingPCHDD;
            }
            catch (Exception ex)
            {
                // Обработка исключения, логирование ошибки и т.д.
                throw new ApplicationException("Error occurred while getting or creating PCHDD.", ex);
            }
        }

        private PowerSupplie GetOrCreatePowerSupply(string bpName)
        {
            try
            {
                var existingPCBP = _applicationContext.PowerSupplies.FirstOrDefault(p => p.bpName == bpName);
                if (existingPCBP == null)
                {
                    // Создаем и сохраняем новую сущность PowerSupplie
                    var newPowerSupplie = new PowerSupplie { bpName = bpName?.Trim().ToUpper() };
                    _applicationContext.Add(newPowerSupplie);
                    _applicationContext.SaveChanges();
                    return newPowerSupplie;
                }

                return existingPCBP;
            }
            catch (Exception ex)
            {
                // Обработка исключения, логирование ошибки и т.д.
                throw new ApplicationException("Error occurred while getting or creating PowerSupplie.", ex);
            }
        }



        private PCMotherboard GetOrCreatePCMotherboard(string motherboard)
        {
            try
            {
                var existingPCMotherboard = _applicationContext.Motherboards.FirstOrDefault(m => m.motherboardName == motherboard);
                if (existingPCMotherboard == null)
                {
                    // Создаем и сохраняем новую сущность PCMotherboard
                    var newPCMotherboard = new PCMotherboard { motherboardName = motherboard?.Trim().ToUpper() };
                    _applicationContext.Add(newPCMotherboard);
                    _applicationContext.SaveChanges();
                    return newPCMotherboard;
                }

                return existingPCMotherboard;
            }
            catch (Exception ex)
            {
                // Обработка исключения, логирование ошибки и т.д.
                throw new ApplicationException("Error occurred while getting or creating PCMotherboard.", ex);
            }
        }

        private PCRam GetOrCreatePCRam(string ram)
        {
            try
            {
                var existingPCRAM = _applicationContext.RAMS.FirstOrDefault(r => r.RAM == ram);
                if (existingPCRAM == null)
                {
                    // Создаем и сохраняем новую сущность PCRam
                    var newPCRam = new PCRam { RAM = ram?.Trim().ToUpper() };
                    _applicationContext.Add(newPCRam);
                    _applicationContext.SaveChanges();
                    return newPCRam;
                }

                return existingPCRAM;
            }
            catch (Exception ex)
            {
                // Обработка исключения, логирование ошибки и т.д.
                throw new ApplicationException("Error occurred while getting or creating PCRam.", ex);
            }
        }

        private PCSSD GetOrCreatePCSSD(string ssd)
        {
            try
            {
                var existingPCSSD = _applicationContext.SSDs.FirstOrDefault(s => s.SSD == ssd);
                if (existingPCSSD == null)
                {
                    // Создаем и сохраняем новую сущность PCSSD
                    var newPCSSD = new PCSSD { SSD = ssd?.Trim().ToUpper() };
                    _applicationContext.Add(newPCSSD);
                    _applicationContext.SaveChanges();
                    return newPCSSD;
                }

                return existingPCSSD;
            }
            catch (Exception ex)
            {
                // Обработка исключения, логирование ошибки и т.д.
                throw new ApplicationException("Error occurred while getting or creating PCSSD.", ex);
            }
        }

        private PCVideocard GetOrCreatePCVideocard(string graphicsCard)
        {
            try
            {
                var existingPCVideocard = _applicationContext.Videocards.FirstOrDefault(v => v.graphicsCard == graphicsCard);
                if (existingPCVideocard == null)
                {
                    // Создаем и сохраняем новую сущность PCVideocard
                    var newPCVideocard = new PCVideocard { graphicsCard = graphicsCard?.Trim().ToUpper() };
                    _applicationContext.Add(newPCVideocard);
                    _applicationContext.SaveChanges();
                    return newPCVideocard;
                }

                return existingPCVideocard;
            }
            catch (Exception ex)
            {
                // Обработка исключения, логирование ошибки и т.д.
                throw new ApplicationException("Error occurred while getting or creating PCVideocard.", ex);
            }
        }



        private string GenerateNextPCID()
        {
            var lastComputer = _applicationContext.Computers
                .OrderByDescending(c => c.ID)
                .FirstOrDefault();

            int autoIncrement = 1000;

            if (lastComputer != null)
            {
                autoIncrement = int.Parse(lastComputer.ID.Substring(1)) + 1;
            }

            string pcID = "Y" + autoIncrement.ToString();
            autoIncrement++;

            return pcID;
        }


        [HttpGet]
        public IActionResult Printers(string selectedOption, string search)
        {
            var printers = _applicationContext.Printers.ToList();

            if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                switch (selectedOption)
                {
                    case "ID":
                        printers = printers.Where(p => p.ID != null && p.ID.ToString() == search).ToList();
                        break;
                    case "Марка":
                        printers = printers.Where(p => p.MarkPrinter != null && p.MarkPrinter.ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "Имя хоста":
                        printers = printers.Where(p => p.HostName != null && p.HostName.ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "Серийный номер":
                        printers = printers.Where(p => p.snNumberPrinter != null && p.snNumberPrinter.ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "Инвентарный номер":
                        printers = printers.Where(p => p.InventoryNumberPrinter != null && p.InventoryNumberPrinter.ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "IP-адрес":
                        printers = printers.Where(p => p.IPAddressPrinter != null && p.IPAddressPrinter.ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "Кабинет":
                        printers = printers.Where(p => p.Cabinet != null && p.Cabinet.ToLower().Contains(search.ToLower())).ToList();
                        break;
                    default:
                        break;
                }
            }


            foreach (var printer in printers)
            {
                model.PrinterViews?.Add(new PrinterView
                {
                    ID = printer.ID,
                    MarkPrinter = printer.MarkPrinter,
                    HostName = printer.HostName,
                    snNumberPrinter = printer.snNumberPrinter,
                    InventoryNumberPrinter = printer.InventoryNumberPrinter,
                    IPAddressPrinter = printer.IPAddressPrinter,
                    Cabinet = printer.Cabinet
                });
            }

            return View(model);
        }



        [HttpGet]
        public IActionResult AddPrinter()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddPrinter([FromForm] string? printerMark, string? hostName, string? snNumber, string? inventoryNumber, string? ipAddress, string? cabinet)
        {
            try
            {
                if (string.IsNullOrEmpty(printerMark) && string.IsNullOrEmpty(hostName) && string.IsNullOrEmpty(snNumber) &&
                    string.IsNullOrEmpty(inventoryNumber) && string.IsNullOrEmpty(ipAddress) && string.IsNullOrEmpty(cabinet))
                {
                    TempData["ErrorMessage"] = "Необходимо заполнить все поля";
                    return View();
                }
                else
                {
                    // Retrieve the maximum ID from the database
                    int maxPrinterId = _applicationContext.Printers.Max(p => (int?)p.ID) ?? 0;


                    var newPrinter = new Printer
                    {
                        ID = maxPrinterId + 1, // Manually set the new ID to be max ID + 1
                        MarkPrinter = printerMark?.Trim(),
                        HostName = hostName?.Trim(),
                        snNumberPrinter = snNumber?.Trim(),
                        InventoryNumberPrinter = inventoryNumber?.Trim(),
                        IPAddressPrinter = ipAddress?.Trim(),
                        Cabinet = cabinet?.Trim(),
                    };

                    _applicationContext.Add(newPrinter);
                    _applicationContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }

            return RedirectToAction("Printers");
        }



        [HttpGet]
        public IActionResult Users(string selectedOption, string search)
        {
            var users = _applicationContext.Users.ToList();

            if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                switch (selectedOption)
                {
                    case "ID":
                        users = users.Where(u => u.ID != null && u.ID.ToString() == search).ToList();
                        break;
                    case "Табельный номер":
                        users = users.Where(u => u.TableNumber != null && u.TableNumber.Trim().ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "ФИО":
                        users = users.Where(u => u.UserName != null && u.UserName.Trim().ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "Кабинет":
                        users = users.Where(u => u.Cabinet != null && u.Cabinet.Trim().ToLower() == search.ToLower()).ToList();
                        break;
                    case "Должность":
                        users = users.Where(u =>
                            u.IDPosition != null &&
                            _applicationContext.Positions.FirstOrDefault(p => p.ID == u.IDPosition)?.positionName?.Trim().ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "Отдел(сокр.)":
                        users = users.Where(u =>
                            u.IDDepartment != null &&
                            _applicationContext.Departments.FirstOrDefault(d => d.ID == u.IDDepartment)?.ShortName?.Trim().ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "Учётная запись":
                        users = users.Where(u => u.Account != null && u.Account.Trim().ToLower().Contains(search.ToLower())).ToList();
                        break;
                    default:
                        break;
                }
            }




            foreach (var user in users)
            {

                string? position = _applicationContext.Positions.Find(user.IDPosition)?.positionName;
                string? department = _applicationContext.Departments.Find(user.IDDepartment)?.ShortName;

                model.UserViews?.Add(new UserView
                {
                    ID = user.ID,
                    TableNumber = user.TableNumber,
                    UserName = user.UserName,
                    Cabinet = user.Cabinet,
                    Position = position,
                    Department = department,
                    Account = user.Account,
                    pcID = user.pcID
                });

            }


            return View(model);

        }


        [HttpGet]
        public IActionResult Departments(string selectedOption, string search)
        {
            var departments = _applicationContext.Departments.ToList();

            if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                switch (selectedOption)
                {
                    case "ID":
                        departments = departments.Where(d => d.ID != null && d.ID.ToString() == search).ToList();
                        break;
                    case "Сокращённое наименование":
                        departments = departments.Where(d => d.ShortName != null && d.ShortName.Trim().ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "Полное наименование":
                        departments = departments.Where(d => d.FullName != null && d.FullName.Trim().ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "Начальник":
                        departments = departments.Where(d =>
                            d.ManagerID != null &&
                            _applicationContext.Chiefs.FirstOrDefault(c => c.ID == d.ManagerID)?.Name?.Trim().ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    default:
                        break;
                }
            }



            foreach (var department in departments)
            {
                string? chiefsName = _applicationContext.Chiefs.Find(department.ManagerID)?.Name;

                model.DepartmentViews?.Add(new DepartmentView
                {
                    ID = department.ID,
                    shortName = department.ShortName,
                    fullName = department.FullName,
                    ChiefsName = chiefsName
                });

            }


            return View(model);
        }



        [HttpGet]
        public IActionResult Monitors(string selectedOption, string search)
        {

            var monitors = _applicationContext.Monitors.ToList();

            if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                switch (selectedOption)
                {
                    case "ID":
                        monitors = monitors.Where(m => m.ID != null && m.ID.ToString().Trim() == search.ToLower()).ToList();
                        break;
                    case "Марка монитора":
                        monitors = monitors.Where(m =>
                            m.markID != null &&
                            _applicationContext.MonitorMarks.FirstOrDefault(mark => mark.ID == m.markID)
                                ?.markMonitor?.Trim().ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "Поставщик":
                        monitors = monitors.Where(m =>
                            m.providerID != null &&
                            _applicationContext.PСProviders.FirstOrDefault(provider => provider.ID == m.providerID)
                                ?.providerName?.Trim().ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "SN монитора":
                        monitors = monitors.Where(m =>
                            m.monitorSN != null &&
                            m.monitorSN.Trim().ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "Инвентарный номер":
                        monitors = monitors.Where(m =>
                            m.inventoryNumberID != null &&
                            _applicationContext.InventoryNumbers.FirstOrDefault(inventory => inventory.ID == m.inventoryNumberID)
                                ?.inventoryNumber?.Trim().ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "ID ПК":
                        monitors = monitors.Where(m =>
                            m.pcID != null &&
                            m.pcID.ToString().Trim() == search).ToList();
                        break;
                    default:
                        break;
                }
            }





            foreach (var monitor in monitors)
            {

                string? markMonitor = _applicationContext.MonitorMarks?.Find(monitor.markID)?.markMonitor;
                string? provider = _applicationContext.PСProviders?.Find(monitor.providerID)?.providerName;
                string? inventoryNumber = _applicationContext.InventoryNumbers?.Find(monitor.inventoryNumberID)?.inventoryNumber;

                model.MonitorViews?.Add(new MonitorView
                {
                    ID = monitor.ID,
                    MarkMonitor = markMonitor,
                    Provider = provider,
                    SNMonitor = monitor.monitorSN,
                    InventoryNumber = inventoryNumber,
                    pcID = monitor.pcID,
                });

            }



            return View(model);
        }



        [HttpGet]
        public IActionResult Components()
        {

            var components = _applicationContext.PСComponents.ToList();


            foreach (var component in components)
            {

                string? processor = _applicationContext.Processors?.Find(component.processorID)?.processorName;
                string? motherboard = _applicationContext.Motherboards?.Find(component.motherboardID)?.motherboardName;
                string? ram = _applicationContext.RAMS?.Find(component.ramID)?.RAM;
                string? videocard = _applicationContext.Videocards?.Find(component.graphicsCardID)?.graphicsCard;
                string? hdd = _applicationContext.HDDs?.Find(component.hddID)?.HDD;
                string? ssd = _applicationContext.SSDs?.Find(component.ssdID)?.SSD;
                string? bp = _applicationContext.PowerSupplies?.Find(component.bpID)?.bpName;

                model.ComponentViews?.Add(new ComponentView
                {
                    ID = component.ID,
                    Processor = processor,
                    Motherboard = motherboard,
                    RAM = ram,
                    Videocard = videocard,
                    HDD = hdd,
                    SSD = ssd,
                    BP = bp
                });

            }



            return View(model);
        }

        [HttpPost]
        public IActionResult AddComponent()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Assemblies(string selectedOption, string search)
        {
            var assemblies = _applicationContext.Assemblies.ToList();

            if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                switch (selectedOption)
                {
                    case "ID":
                        assemblies = assemblies.Where(a => a.ID != null && a.ID.ToString().Trim() == search.Trim()).ToList();
                        break;
                    case "Процессор":
                        assemblies = assemblies.Where(a => a.processorID != null && _applicationContext.Processors.FirstOrDefault(p => p.ID == a.processorID)?.processorName?.ToLower().Trim().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "Материнская плата":
                        assemblies = assemblies.Where(a => a.motherboardID != null && _applicationContext.Motherboards.FirstOrDefault(m => m.ID == a.motherboardID)?.motherboardName?.ToLower().Trim().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "ОЗУ":
                        assemblies = assemblies.Where(a => a.ramID != null && _applicationContext.RAMS.FirstOrDefault(r => r.ID == a.ramID)?.RAM?.ToLower().Trim().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "Видеокарта":
                        assemblies = assemblies.Where(a => a.graphicsCardID != null && _applicationContext.Videocards.FirstOrDefault(v => v.ID == a.graphicsCardID)?.graphicsCard?.ToLower().Trim().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "HDD":
                        assemblies = assemblies.Where(a => a.hddID != null && _applicationContext.HDDs.FirstOrDefault(h => h.ID == a.hddID)?.HDD?.ToLower().Trim().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "SSD":
                        assemblies = assemblies.Where(a => a.ssdID != null && _applicationContext.SSDs.FirstOrDefault(s => s.ID == a.ssdID)?.SSD?.ToLower().Trim().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "Блок питания":
                        assemblies = assemblies.Where(a => a.bpID != null && _applicationContext.PowerSupplies.FirstOrDefault(p => p.ID == a.bpID)?.bpName?.ToLower().Trim().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "ID ПК":
                        assemblies = assemblies.Where(a => a.pcID != null && a.pcID.ToString().Trim() == search.Trim()).ToList();
                        break;
                    default:
                        break;
                }
            }



            foreach (var assembly in assemblies)
            {

                string? processor = _applicationContext.Processors.Find(assembly.processorID)?.processorName;
                string? motherboard = _applicationContext.Motherboards.Find(assembly.motherboardID)?.motherboardName;
                string? ram = _applicationContext.RAMS.Find(assembly.ramID)?.RAM;
                string? videocard = _applicationContext.Videocards.Find(assembly.graphicsCardID)?.graphicsCard;
                string? hdd = _applicationContext.HDDs.Find(assembly.hddID)?.HDD;
                string? ssd = _applicationContext.SSDs.Find(assembly.ssdID)?.SSD;
                string? bp = _applicationContext.PowerSupplies.Find(assembly.bpID)?.bpName;


                model.AssemblyViews?.Add(new AssemblyView
                {
                    ID = assembly.ID,
                    Processor = processor,
                    Motherboard = motherboard,
                    RAM = ram,
                    Videocard = videocard,
                    HDD = hdd,
                    SSD = ssd,
                    BP = bp,
                    pcID = assembly.pcID

                });

            }


            return View(model);
        }



        [HttpGet]
        public IActionResult OperationSystems(string selectedOption, string search)
        {

            var operationSystems = _applicationContext.OC.ToList();

            if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                switch (selectedOption)
                {
                    case "ID":
                        operationSystems = operationSystems.Where(o => o.ID != null && o.ID.ToString().Trim() == search.ToLower()).ToList();
                        break;
                    case "Название ОС":
                        operationSystems = operationSystems.Where(o =>
                            o.OCName != null &&
                            o.OCName.Trim().ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "ID ПК":
                        operationSystems = operationSystems.Where(o => o.pcID != null && o.pcID.ToString().Trim() == search).ToList();
                        break;
                    default:
                        break;
                }
            }




            foreach (var OS in operationSystems)
            {

                model.OSViews?.Add(new OSView
                {
                    ID = OS.ID,
                    osName = OS.OCName,
                    pcID = OS.pcID
                });

            }


            return View(model);
        }


        [HttpGet]
        public IActionResult MOLS(string selectedOption, string search)
        {

            var MOLs = _applicationContext.MOLs.ToList();

            if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                switch (selectedOption)
                {
                    case "ID":
                        MOLs = MOLs.Where(m => m.ID != null && m.ID.ToString() == search).ToList();
                        break;
                    case "ФИО":
                        MOLs = MOLs.Where(m =>
                            m.userName != null &&
                            m.userName.Trim().ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "ID ПК":
                        MOLs = MOLs.Where(m => m.pcID != null && m.pcID.ToString() == search).ToList();
                        break;
                    default:
                        break;
                }
            }



            foreach (var mol in MOLs)
            {

                model.MOLViews?.Add(new MOLView
                {
                    ID = mol.ID,
                    UserName = mol.userName,
                    pcID = mol.pcID
                });

            }


            return View(model);
        }



        [HttpGet]
        public IActionResult EditDevice(int id)
        {
            try
            {
                var data = _applicationContext.AdditionalDevices.Find(id);

                if (data != null)
                {
                    var keyboard = _applicationContext.KeyboardsAndMouses.Find(data.keyboardMouseID);
                    var webcam = _applicationContext.Webcams.Find(data.webcamID);
                    var headphone = _applicationContext.Headphones.Find(data.headphonesID);
                    var networkFilter = _applicationContext.NetworkFilters.Find(data.networkFilterID);

                    model.AdditionalDevicesViews?.Add(new AdditionalDevicesView
                    {
                        KeyboardAndMouse = keyboard?.keyboardMouse, // Предполагаем, что у вас есть столбец "Name" в таблице "Keyboards"
                        Webcam = webcam?.webcam, // Предполагаем, что у вас есть столбец "Name" в таблице "Webcams"
                        Headphone = headphone?.headphones, // Предполагаем, что у вас есть столбец "Name" в таблице "Headphones"
                        NetworkFilter = networkFilter?.networkFilter, // Предполагаем, что у вас есть столбец "Name" в таблице "NetworkFilters"
                        pcID = data.pcID
                    });
                }

                return View(model);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }
        }


        [HttpPost]
        public IActionResult EditDevice([FromForm] string keyboardName, [FromForm] string webcamName, [FromForm] string headphoneName, [FromForm] string netfilterName, [FromForm] int pcID, int id)
        {
            var additionalDevice = _applicationContext.AdditionalDevices.FirstOrDefault(a => a.ID == id);

            if (additionalDevice == null)
            {
                return NotFound();
            }

            int? keyboardID = additionalDevice.keyboardMouseID;

            if (keyboardID.HasValue)
            {
                var keyboard = _applicationContext.KeyboardsAndMouses.FirstOrDefault(k => k.ID == keyboardID.Value);

                if (keyboard != null)
                {
                    keyboard.keyboardMouse = keyboardName;
                }
            }

            // Аналогично обрабатываем webcamName
            int? webcamID = additionalDevice.webcamID;

            if (webcamID.HasValue)
            {
                var webcam = _applicationContext.Webcams.FirstOrDefault(w => w.ID == webcamID.Value);

                if (webcam != null)
                {
                    webcam.webcam = webcamName;
                }
            }

            // Аналогично обрабатываем headphoneName
            int? headphoneID = additionalDevice.headphonesID;

            if (headphoneID.HasValue)
            {
                var headphone = _applicationContext.Headphones.FirstOrDefault(h => h.ID == headphoneID.Value);

                if (headphone != null)
                {
                    headphone.headphones = headphoneName;
                }
            }

            // Аналогично обрабатываем netfilterName
            int? netfilterID = additionalDevice.networkFilterID;

            if (netfilterID.HasValue)
            {
                var netfilter = _applicationContext.NetworkFilters.FirstOrDefault(n => n.ID == netfilterID.Value);

                if (netfilter != null)
                {
                    netfilter.networkFilter = netfilterName;
                }
            }

            _applicationContext.SaveChanges();

            return RedirectToAction("AdditionalDevices");
        }


        [HttpGet]
        public IActionResult EditPrinter(int id)
        {

            try
            {
                var data = _applicationContext.Printers.Find(id);

                if (data != null)
                {
                    model.PrinterViews?.Add(new PrinterView
                    {
                        MarkPrinter = data.MarkPrinter,
                        HostName = data.HostName,
                        snNumberPrinter = data.snNumberPrinter,
                        InventoryNumberPrinter = data.InventoryNumberPrinter,
                        IPAddressPrinter = data.IPAddressPrinter,
                        Cabinet = data.Cabinet
                    });
                }

                return View(model);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

        }


        [HttpPost]
        public IActionResult EditPrinter([FromForm] string printerMark, string hostName, string snNumber, string inventoryNumber, string ipAddress, string cabinet, int id)
        {
            try
            {
                var data = _applicationContext.Printers.Find(id);


                if (data != null)
                {


                    data.IPAddressPrinter = ipAddress;
                    data.MarkPrinter = printerMark;
                    data.snNumberPrinter = snNumber;
                    data.HostName = hostName;
                    data.InventoryNumberPrinter = inventoryNumber;
                    data.Cabinet = cabinet;

                    _applicationContext.Printers.Update(data);
                    _applicationContext.SaveChanges();

                }

                return RedirectToAction("Printers");
            }

            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }


        }



        [HttpPost]
        public IActionResult DeleteDevice(int id)
        {
            var device = _applicationContext.AdditionalDevices.FirstOrDefault(d => d.ID == id);


            if (device != null)
            {
                device.keyboardMouseID = null;
                device.webcamID = null;
                device.networkFilterID = null;
                device.headphonesID = null;

                _applicationContext.SaveChanges();
            }


            return RedirectToAction("AdditionalDevices");
        }



        [HttpPost]
        public IActionResult DeleteMol(int id)
        {
            var mol = _applicationContext.MOLs.FirstOrDefault(m => m.ID == id);


            if (mol != null)
            {
                mol.userName = string.Empty;
                _applicationContext.SaveChanges();
            }


            return RedirectToAction("MOLS");
        }



        [HttpPost]
        public IActionResult DeleteUser(int id)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.ID == id);


            if (user != null)
            {
                user.TableNumber = string.Empty;
                user.UserName = string.Empty;
                user.Cabinet = string.Empty;
                user.IDPosition = null;
                user.IDDepartment = null;
                user.Account = string.Empty;

                _applicationContext.SaveChanges();
            }


            return RedirectToAction("Users");
        }


        [HttpPost]
        public IActionResult DeleteInventoryNumber(int id)
        {
            var inventoryNumbers = _applicationContext.InventoryNumbers.FirstOrDefault(i => i.ID == id);

            if (inventoryNumbers != null)
            {
                inventoryNumbers.inventoryNumber = string.Empty;
                _applicationContext.SaveChanges();
            }


            return RedirectToAction("InventoryNumbers");
        }


        [HttpPost]
        public IActionResult DeleteAssembly(int id)
        {
            var assembly = _applicationContext.Assemblies.FirstOrDefault(a => a.ID == id);


            if (assembly != null)
            {

                assembly.processorID = null;
                assembly.motherboardID = null;
                assembly.ramID = null;
                assembly.graphicsCardID = null;
                assembly.hddID = null;
                assembly.ssdID = null;
                assembly.bpID = null;

                _applicationContext.SaveChanges();
            }



            return RedirectToAction("Assemblies");
        }



        [HttpGet]
        public IActionResult EditInventoryNumber(int id)
        {

            try
            {
                var data = _applicationContext.InventoryNumbers.Find(id);

                if (data != null)
                {
                    model.InventoryNumberViews?.Add(new InventoryNumberView
                    {
                        InventoryNumbers = data.inventoryNumber,
                        pcID = data.pcID,
                        monitorID = data.monitorID

                    });
                }

                return View(model);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                return View();
            }

        }




        [HttpGet]
        public IActionResult InventoryNumbers(string selectedOption, string search)
        {

            var inventoryNumbers = _applicationContext.InventoryNumbers.ToList();

            if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                switch (selectedOption)
                {
                    case "ID":
                        inventoryNumbers = inventoryNumbers.Where(i => i.ID != null && i.ID.ToString() == search).ToList();
                        break;
                    case "Инвентарный номер":
                        inventoryNumbers = inventoryNumbers.Where(i =>
                            i.inventoryNumber != null &&
                            i.inventoryNumber.Trim().ToLower().Contains(search.ToLower())).ToList();
                        break;
                    case "ID ПК":
                        inventoryNumbers = inventoryNumbers.Where(i => i.pcID != null && i.pcID.ToString() == search).ToList();
                        break;
                    case "ID Монитора":
                        inventoryNumbers = inventoryNumbers.Where(i => i.monitorID != null && i.monitorID.ToString() == search).ToList();
                        break;
                    default:
                        break;
                }
            }


            foreach (var inventoryNumber in inventoryNumbers)
            {

                model.InventoryNumberViews?.Add(new InventoryNumberView
                {
                    ID = inventoryNumber.ID,
                    InventoryNumbers = inventoryNumber.inventoryNumber,
                    pcID = inventoryNumber.pcID,
                    monitorID = inventoryNumber.monitorID
                });

            }


            return View(model);
        }


        [HttpGet]
        public IActionResult AdditionalDevices(string selectedOption, string search)
        {
            var additionalDevices = _applicationContext.AdditionalDevices.ToList();

            if (!string.IsNullOrEmpty(selectedOption) && !string.IsNullOrEmpty(search))
            {
                search = search.Trim();

                switch (selectedOption)
                {
                    case "ID":
                        additionalDevices = additionalDevices.Where(d => d.ID != null && d.ID.ToString() == search).ToList();
                        break;
                    case "Клавиатура+Мышь":
                        additionalDevices = additionalDevices.Where(d =>
                            d.keyboardMouseID != null &&
                            _applicationContext.KeyboardsAndMouses
                                .FirstOrDefault(km => km.ID == d.keyboardMouseID)?.keyboardMouse
                                ?.ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "Веб-камера":
                        additionalDevices = additionalDevices.Where(d =>
                            d.webcamID != null &&
                            _applicationContext.Webcams
                                .FirstOrDefault(wb => wb.ID == d.webcamID)?.webcam
                                ?.ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "Наушник":
                        additionalDevices = additionalDevices.Where(d =>
                            d.headphonesID != null &&
                            _applicationContext.Headphones
                                .FirstOrDefault(h => h.ID == d.headphonesID)?.headphones
                                ?.ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "Сетевой фильтр":
                        additionalDevices = additionalDevices.Where(d =>
                            d.networkFilterID != null &&
                            _applicationContext.NetworkFilters
                                .FirstOrDefault(nf => nf.ID == d.networkFilterID)?.networkFilter
                                ?.ToLower().Contains(search.ToLower()) == true).ToList();
                        break;
                    case "ID ПК":
                        additionalDevices = additionalDevices.Where(d =>
                            d.pcID != null && d.pcID.ToString() == search).ToList();
                        break;
                    default:
                        break;
                }
            }




            foreach (var device in additionalDevices)
            {
                var keyboardAndMouse = _applicationContext.KeyboardsAndMouses
                    .FirstOrDefault(km => km.ID == device.keyboardMouseID)?.keyboardMouse;

                var webcam = _applicationContext.Webcams
                    .FirstOrDefault(w => w.ID == device.webcamID)?.webcam;

                var headphone = _applicationContext.Headphones
                    .FirstOrDefault(h => h.ID == device.headphonesID)?.headphones;

                var networkfilter = _applicationContext.NetworkFilters
                    .FirstOrDefault(nf => nf.ID == device.networkFilterID)?.networkFilter;

                model.AdditionalDevicesViews?.Add(new AdditionalDevicesView
                {
                    ID = (int)device.ID,
                    KeyboardAndMouse = keyboardAndMouse,
                    Webcam = webcam,
                    Headphone = headphone,
                    NetworkFilter = networkfilter,
                    pcID = device.pcID
                });
            }

            return View(model);
        }



        [HttpGet]
        public IActionResult Main()
        {
            return View();
        }



        [HttpGet]
        public IActionResult EditUser(int? id)
        {

            var user = _applicationContext.Users.Find(id);

            var department = _applicationContext.Departments?.Find(user?.IDDepartment)?.ShortName?.Trim();
            var position = _applicationContext.Positions?.Find(user?.IDPosition)?.positionName?.Trim();

            if (user != null)
            {
                model.UserViews?.Add(new UserView
                {
                    UserName = user.UserName,
                    TableNumber = user.TableNumber,
                    Cabinet = user.Cabinet,
                    Position = position,
                    Department = department,
                    Account = user.Account
                });
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult EditDepartment(int? id)
        {
            var department = _applicationContext.Departments.Find(id);

            if (department != null)
            {
                model.DepartmentViews?.Add(new DepartmentView
                {
                    ID = department.ID,
                    shortName = department.ShortName?.Trim(),
                    fullName = department.FullName?.Trim()
                });
            }

            return View(model);

        }



        [HttpPost]
        public IActionResult DeletePC(string id)
        {
            var computer = _applicationContext.Computers.FirstOrDefault(c => c.ID == id);

            if (computer != null)
            {
                computer.pcName = string.Empty;
                computer.issueDate = string.Empty;
                computer.userID = null;
                computer.departmentID = null;
                computer.markID = null;
                computer.snSystemBlock = string.Empty;
                computer.assemblyID = null;
                computer.providerID = null;
                computer.categoryID = null;
                computer.ocID = null;
                computer.inventoryNumberID = null;
                computer.skladtNumber = string.Empty;
                computer.monitorID = null;
                computer.monitorSN = string.Empty;
                computer.additionallDeviceID = null;
                computer.cabinet = string.Empty;
                computer.molID = null;
                computer.projectID = null;
                computer.foundation = string.Empty;
                computer.note = string.Empty;

                _applicationContext.SaveChanges();
            }


            return RedirectToAction("Computers");
        }



        [HttpPost]
        public IActionResult DeleteDepartment(int id)
        {
            var department = _applicationContext.Departments.FirstOrDefault(d => d.ID == id);

            if (department != null)
            {
                department.ShortName = string.Empty;
                department.FullName = string.Empty;
                department.ManagerID = null;

                _applicationContext.SaveChanges();
            }

            return RedirectToAction("Departments");

        }


        [HttpPost]
        public IActionResult DeletePrinter(int id)
        {

            var printer = _applicationContext.Printers.FirstOrDefault(p => p.ID == id);

            if (printer != null)
            {
                printer.MarkPrinter = string.Empty;
                printer.HostName = string.Empty;
                printer.snNumberPrinter = string.Empty;
                printer.InventoryNumberPrinter = string.Empty;
                printer.IPAddressPrinter = string.Empty;
                printer.Cabinet = string.Empty;


                _applicationContext.SaveChanges();
            }

            return RedirectToAction("Printers");
        }

        [HttpPost]
        public IActionResult DeleteOS(int id)
        {
            var os = _applicationContext.OC.FirstOrDefault(o => o.ID == id);

            if (os != null)
            {
                os.OCName = string.Empty;

                _applicationContext.SaveChanges();
            }


            return RedirectToAction("OperationSystems");
        }



        [HttpPost]
        public IActionResult DeleteMonitor(int id)
        {
            var monitor = _applicationContext.Monitors.FirstOrDefault(m => m.ID == id);

            if (monitor != null)
            {
                monitor.markID = null;
                monitor.providerID = null;
                monitor.monitorSN = string.Empty;
                monitor.inventoryNumberID = null;

                _applicationContext.SaveChanges();
            }

            return RedirectToAction("Monitors");
        }



        [HttpPost]
        public IActionResult EditDepartment([FromForm] string shortName, string fullName, string ChiefsName, int? id)
        {
            var exisitingShortDepartment = _applicationContext.Departments.FirstOrDefault(d => d.ShortName.Trim() == shortName.Trim());


            Department newDepartment = exisitingShortDepartment ?? new Department { ShortName = shortName.Trim().ToLower() };

            if (exisitingShortDepartment == null)
            {
                _applicationContext.Departments.Add(newDepartment);
            }

            _applicationContext.SaveChanges();



            var chiefName = new Chief
            {
                Name = ChiefsName
            };

            _applicationContext.Add(chiefName);
            _applicationContext.SaveChanges();


            var department = _applicationContext.Departments.Find(id);

            if (department != null)
            {

                department.FullName = fullName;
                department.ManagerID = chiefName.ID;

                _applicationContext.Departments.Update(department);
                _applicationContext.SaveChanges();
            }


            return RedirectToAction("Departments");

        }
        [HttpPost]
        public IActionResult EditPC(string id, [FromForm] string pcName, [FromForm] string dateOfIssue, [FromForm] string userName, [FromForm] string departmentName, [FromForm] string pcMark, [FromForm] string snSystemOfBlock, [FromForm] string characteristic, [FromForm] string providerName, [FromForm] string category, [FromForm] string OS, [FromForm] string inventoryNumber, [FromForm] string skladNumberPC, [FromForm] string monitorName, [FromForm] string snMonitor, [FromForm] string additionalDevices, [FromForm] string cabinet, [FromForm] string MOL, [FromForm] string projectName, [FromForm] string foundation, [FromForm] string note)
        {

            var pc = _applicationContext.Computers.FirstOrDefault(c => c.ID == id);

            if (pc == null)
            {
                ViewData["ErrorMessage"] = "Произошла ошибка при обработке данных: ";
                return View();
            }

            pc.pcName = pcName;
            pc.issueDate = dateOfIssue;
            pc.snSystemBlock = snSystemOfBlock;
            pc.skladtNumber = skladNumberPC;
            pc.foundation = foundation;
            pc.note = note;



            var user = _applicationContext.Users.FirstOrDefault(u => u.ID == pc.userID);
            if (user != null)
            {
                user.UserName = userName;
                user.Cabinet = cabinet;

                var existingDepartment = _applicationContext.Departments.FirstOrDefault(d => d.ShortName == departmentName);
                if (existingDepartment == null)
                {
                    try
                    {
                        var newDepartment = new Department { ShortName = departmentName };
                        _applicationContext.Add(newDepartment);
                        _applicationContext.SaveChanges();
                        user.IDDepartment = newDepartment.ID;
                    }
                    catch (Exception ex)
                    {
                        // Handle the exception here, log it, throw a custom exception, etc.
                        throw new ApplicationException("Error occurred while creating a new department.", ex);
                    }
                }
                else
                {
                    user.IDDepartment = existingDepartment.ID;
                }
            }

            // Update inventory number
            var invent = _applicationContext.InventoryNumbers.FirstOrDefault(i => i.inventoryNumber == inventoryNumber);
            if (invent == null)
            {
                try
                {
                    var newInventoryNumber = new InventoryNumber { inventoryNumber = inventoryNumber };
                    _applicationContext.Add(newInventoryNumber);
                    _applicationContext.SaveChanges();
                    pc.inventoryNumberID = newInventoryNumber.ID;
                }
                catch (Exception ex)
                {
                    // Handle the exception here, log it, throw a custom exception, etc.
                    throw new ApplicationException("Error occurred while creating a new inventory number.", ex);
                }
            }
            else
            {
                pc.inventoryNumberID = invent.ID;
            }

            // Update PCMark
            var markPC = _applicationContext.PCMarks.FirstOrDefault(m => m.ID == pc.markID);
            if (markPC != null)
            {
                // Обновляем значение pcMark только если pcMark не равен null
                if (!string.IsNullOrEmpty(pcMark))
                {
                    markPC.pcMark = pcMark;
                }
            }


            // Update monitor's name and serial number
            var monitor = _applicationContext.Monitors.FirstOrDefault(m => m.ID == pc.monitorID);
            if (monitor != null)
            {
                var MonitorName = _applicationContext.MonitorMarks.Find(monitor.markID);

                if (MonitorName != null)
                {
                    // Обновляем значение markMonitor только если monitorName не равен null
                    if (!string.IsNullOrEmpty(monitorName))
                    {
                        MonitorName.markMonitor = monitorName;
                    }

                    monitor.monitorSN = snMonitor;
                }
            }




            string processor = "";
            string motherboard = "";
            string ram = "";
            string graphicsCard = "";
            string hdd = "";
            string ssd = "";
            string bpName = "";



            if (string.IsNullOrEmpty(characteristic))
            {

            }

            else
            {
                string[] partsOfPC = characteristic.Split('/');


                for (int i = 0; i < partsOfPC.Length; i++)
                {
                    string part = partsOfPC[i].Trim();

                    switch (i)
                    {
                        case 0:
                            processor = part;
                            break;
                        case 1:
                            motherboard = part;
                            break;
                        case 2:
                            ram = part;
                            break;
                        case 3:
                            graphicsCard = part;
                            break;
                        case 4:
                            hdd = part;
                            break;
                        case 5:
                            ssd = part;
                            break;
                        case 6:
                            bpName = part;
                            break;
                    }
                }
            }




            // Получаем ID сборки (assemblyID) из компьютера
            int? assemblyID = pc.assemblyID;

            if (assemblyID.HasValue)
            {
                // Находим сборку (assembly) в базе данных по assemblyID
                var assembly = _applicationContext.Assemblies.FirstOrDefault(a => a.ID == assemblyID);

                if (assembly != null)
                {

                    var processorEntity = _applicationContext.Processors.FirstOrDefault(p => p.ID == assembly.processorID);
                    if (processorEntity != null)
                    {
                        processorEntity.processorName = processor;
                    }

                    // Находим и обновляем каждую компоненту по ID
                    var motherboardEntity = _applicationContext.Motherboards.FirstOrDefault(m => m.ID == assembly.motherboardID);
                    if (motherboardEntity != null)
                    {
                        motherboardEntity.motherboardName = motherboard;
                    }

                    var ramEntity = _applicationContext.RAMS.FirstOrDefault(r => r.ID == assembly.ramID);
                    if (ramEntity != null)
                    {
                        ramEntity.RAM = ram;
                    }

                    var graphicsCardEntity = _applicationContext.Videocards.FirstOrDefault(g => g.ID == assembly.graphicsCardID);
                    if (graphicsCardEntity != null)
                    {
                        graphicsCardEntity.graphicsCard = graphicsCard;
                    }

                    var hddEntity = _applicationContext.HDDs.FirstOrDefault(h => h.ID == assembly.hddID);
                    if (hddEntity != null)
                    {
                        hddEntity.HDD = hdd;
                    }

                    var ssdEntity = _applicationContext.SSDs.FirstOrDefault(s => s.ID == assembly.ssdID);
                    if (ssdEntity != null)
                    {
                        ssdEntity.SSD = ssd;
                    }

                    var powerSupplyEntity = _applicationContext.PowerSupplies.FirstOrDefault(p => p.ID == assembly.bpID);
                    if (powerSupplyEntity != null)
                    {
                        powerSupplyEntity.bpName = bpName;
                    }

                    _applicationContext.SaveChanges();
                }
            }



            string keyboardAndMouse = "";
            string WebCam = "";
            string Headphone = "";
            string NetworkFilter = "";


            if (string.IsNullOrEmpty(additionalDevices))
            {

            }
            else
            {
                string[] devicesOfPC = additionalDevices.Split('/');


                for (int i = 0; i < devicesOfPC.Length; i++)
                {
                    string device = devicesOfPC[i].Trim();

                    switch (i)
                    {
                        case 0:
                            keyboardAndMouse = device;
                            break;
                        case 1:
                            WebCam = device;
                            break;
                        case 2:
                            Headphone = device;
                            break;
                        case 3:
                            NetworkFilter = device;
                            break;
                    }
                }
            }



            var additionalDevice = _applicationContext.AdditionalDevices
                .FirstOrDefault(a => a.ID == pc.additionallDeviceID);

            if (additionalDevice != null)
            {
                // Обновляем значения устройств в таблице AdditionalDevices
                if (!string.IsNullOrEmpty(keyboardAndMouse))
                {
                    var keyboardEntity = _applicationContext.KeyboardsAndMouses
                        .FirstOrDefault(k => k.ID == additionalDevice.keyboardMouseID);

                    if (keyboardEntity != null)
                    {
                        keyboardEntity.keyboardMouse = keyboardAndMouse?.Trim().ToUpper();
                    }
                }

                if (!string.IsNullOrEmpty(WebCam))
                {
                    var webcamEntity = _applicationContext.Webcams
                        .FirstOrDefault(w => w.ID == additionalDevice.webcamID);

                    if (webcamEntity != null)
                    {
                        webcamEntity.webcam = WebCam.Trim().ToUpper();
                    }
                }

                if (!string.IsNullOrEmpty(Headphone))
                {
                    var headphoneEntity = _applicationContext.Headphones
                        .FirstOrDefault(h => h.ID == additionalDevice.headphonesID);

                    if (headphoneEntity != null)
                    {
                        headphoneEntity.headphones = Headphone?.Trim().ToUpper();
                    }
                }

                if (!string.IsNullOrEmpty(NetworkFilter))
                {
                    var networkFilterEntity = _applicationContext.NetworkFilters
                        .FirstOrDefault(n => n.ID == additionalDevice.networkFilterID);

                    if (networkFilterEntity != null)
                    {
                        networkFilterEntity.networkFilter = NetworkFilter?.Trim().ToUpper();
                    }
                }

                try
                {
                    // Сохраняем изменения в базе данных
                    _applicationContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Обработка ошибки, если необходимо
                    throw;
                }
            }



            var provider = _applicationContext.PСProviders.FirstOrDefault(p => p.ID == pc.providerID);
            if (provider != null)
            {
                if (!string.IsNullOrEmpty(providerName))
                {
                    provider.providerName = providerName;
                }


            }

            // Update PC category
            var pcCategory = _applicationContext.PCCategories.FirstOrDefault(c => c.ID == pc.categoryID);
            if (pcCategory != null)
            {

                if (!string.IsNullOrEmpty(category))
                {
                    pcCategory.pcCategory = category;
                }

            }

            // Update operating system
            var os = _applicationContext.OC.FirstOrDefault(o => o.ID == pc.ocID);
            if (os != null)
            {

                if (!string.IsNullOrEmpty(OS))
                {
                    os.OCName = OS;
                }


            }

            // Update MOL's name
            var mol = _applicationContext.MOLs.FirstOrDefault(m => m.ID == pc.molID);
            if (mol != null)
            {
                if (!string.IsNullOrEmpty(MOL))
                {
                    mol.userName = MOL;
                }

            }

            // Update project
            var project = _applicationContext.Projects.FirstOrDefault(p => p.ID == pc.projectID);
            if (project != null)
            {
                if (!string.IsNullOrEmpty(projectName))
                {
                    project.project = projectName;
                }

            }


            try
            {
                // Сохраняем изменения в базе данных
                _applicationContext.SaveChanges();
            }
            catch (Exception ex)
            {
                // Обработка ошибки, если необходимо
                throw;
            }


            return RedirectToAction("Computers");
        }




        [HttpGet]
        public IActionResult EditPC(string id)
        {
            var pc = _applicationContext.Computers.FirstOrDefault(c => c.ID == id);


            if (pc != null)
            {
                model.PCViews?.Add(new PCView
                {
                    ID = pc.ID,
                    pcName = pc.pcName,
                    dateOfIssue = pc.issueDate,
                    userName = _applicationContext.Users.Find(pc.userID)?.UserName?.Trim(),
                    departmentName = _applicationContext.Departments.Find(pc.departmentID)?.ShortName?.Trim(),
                    pcMark = _applicationContext.PCMarks.Find(pc.markID)?.pcMark?.Trim(),
                    snSystemBlock = pc.snSystemBlock,
                    characteristic = GetPCCharacteristic(pc),
                    providerName = _applicationContext.PСProviders.Find(pc.providerID)?.providerName,
                    category = _applicationContext.PCCategories.Find(pc.categoryID)?.pcCategory,
                    OS = _applicationContext.OC.Find(pc.ocID)?.OCName,
                    inventoryNumber = _applicationContext.InventoryNumbers.Find(pc.inventoryNumberID)?.inventoryNumber,
                    skladNumberPC = pc.skladtNumber,
                    monitorName = GetMonitorMark(pc.monitorID),
                    snMonitor = _applicationContext.Monitors.Find(pc.monitorID)?.monitorSN?.Trim(),
                    additionalDevices = GetAdditionalDevices(pc.additionallDeviceID),
                    cabinet = _applicationContext.Users.Find(pc.userID)?.Cabinet?.Trim(),
                    MOL = _applicationContext.MOLs.Find(pc.molID)?.userName,
                    projectName = _applicationContext.Projects.Find(pc.projectID)?.project,
                    foundation = pc.foundation,
                    note = pc.note
                });

            }
            return View(model);
        }







        [HttpPost]
        public IActionResult EditUser([FromForm] string UserName, string TableNumber, string Cabinet, string Position, string ShortDepartment, string Account, int? id)
        {

            var existingPosition = _applicationContext.Positions.FirstOrDefault(m => m.positionName.Trim() == Position.Trim());

            Position newPosition = existingPosition ?? new Position { positionName = Position.Trim().ToLower() };

            if (existingPosition == null)
            {
                _applicationContext.Positions.Add(newPosition);
            }

            _applicationContext.SaveChanges();


            var PositionID = newPosition.ID;


            var exisitingDepartment = _applicationContext.Departments.FirstOrDefault(m => m.ShortName.Trim() == ShortDepartment.Trim());

            Department newDepartment = exisitingDepartment ?? new Department { ShortName = ShortDepartment.Trim().ToLower() };

            if (exisitingDepartment == null)
            {
                _applicationContext.Departments.Add(newDepartment);
            }

            _applicationContext.SaveChanges();


            var DepartmentID = newDepartment.ID;


            var user = _applicationContext.Users.Find(id);

            if (user != null)
            {
                user.UserName = UserName;
                user.TableNumber = TableNumber;
                user.Cabinet = Cabinet;
                user.IDPosition = PositionID;
                user.IDDepartment = DepartmentID;
                user.Account = Account;

                _applicationContext.Users.Update(user);
                _applicationContext.SaveChanges();
            }


            return RedirectToAction("Users");

        }



        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}