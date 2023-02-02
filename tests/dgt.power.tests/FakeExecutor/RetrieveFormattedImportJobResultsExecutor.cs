using FakeXrmEasy.Abstractions;
using FakeXrmEasy.Abstractions.FakeMessageExecutors;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;

namespace dgt.power.tests.FakeExecutor;

public class RetrieveFormattedImportJobResultsExecutor : IFakeMessageExecutor
{
    public bool CanExecute(OrganizationRequest request)
    {
        return request is RetrieveFormattedImportJobResultsRequest;
    }

    public Type GetResponsibleRequestType()
    {
        return typeof(RetrieveFormattedImportJobResultsRequest);
    }

    public OrganizationResponse Execute(OrganizationRequest request, IXrmFakedContext ctx)
    {
        var typed = (RetrieveFormattedImportJobResultsRequest)request;

        var response = new RetrieveFormattedImportJobResultsResponse
        {
            Results =
            {
                ["FormattedResults"] = "<?xml version=\"1.0\"?><?mso-application progid=\"Excel.Sheet\"?><Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\" xmlns:html=\"http://www.w3.org/TR/REC-html40\"><DocumentProperties xmlns=\"urn:schemas-microsoft-com:office:office\"><Author>XUnit</Author><LastAuthor>XUnit</LastAuthor><Created>"+DateTime.UtcNow.AddMinutes(-10).ToString("yyyy-MM-ddTHH:mm:ss")+"Z</Created><Version>16.00</Version></DocumentProperties><OfficeDocumentSettings xmlns=\"urn:schemas-microsoft-com:office:office\"><AllowPNG/></OfficeDocumentSettings><ExcelWorkbook xmlns=\"urn:schemas-microsoft-com:office:excel\"><WindowHeight>16410</WindowHeight><WindowWidth>31680</WindowWidth><WindowTopX>32767</WindowTopX><WindowTopY>32767</WindowTopY><ProtectStructure>False</ProtectStructure><ProtectWindows>False</ProtectWindows></ExcelWorkbook><Styles><Style ss:ID=\"Default\" ss:Name=\"Normal\"><Alignment ss:Vertical=\"Bottom\"/><Borders/><Font ss:FontName=\"Calibri\" x:Family=\"Swiss\" ss:Size=\"11\" ss:Color=\"#000000\"/><Interior/><NumberFormat/><Protection/></Style></Styles><Worksheet ss:Name=\"XUnit\"><Table ss:ExpandedColumnCount=\"1\" ss:ExpandedRowCount=\"1\" x:FullColumns=\"1\" x:FullRows=\"1\" ss:DefaultRowHeight=\"15\"><Row><Cell><Data ss:Type=\"String\">xunit</Data></Cell></Row></Table><WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\"><PageSetup><Header x:Margin=\"0.3\"/><Footer x:Margin=\"0.3\"/><PageMargins x:Bottom=\"0.75\" x:Left=\"0.7\" x:Right=\"0.7\" x:Top=\"0.75\"/></PageSetup><Selected/><ProtectObjects>False</ProtectObjects><ProtectScenarios>False</ProtectScenarios></WorksheetOptions></Worksheet></Workbook>"
            }
        };
        Thread.Sleep(2000);

        return response;
    }
}