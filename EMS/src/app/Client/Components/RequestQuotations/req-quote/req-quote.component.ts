import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NotificationsService } from 'src/app/Account/Settings/Services/notifications.service';
import { IService } from 'src/app/Admin/Settings/Models/iadmin';
import { IClient, IQuotationLine,IQuotationRequest, IQuotationRequestLine } from 'src/app/Client/Settings/Models/iclient';
import { ClientService } from 'src/app/Client/Settings/Services/client.service';
import { AdminService } from 'src/app/Admin/Settings/Services/admin.service';

@Component({
  selector: 'app-req-quote',
  templateUrl: './req-quote.component.html',
  styleUrls: ['./req-quote.component.scss']
})
export class ReqQuoteComponent implements OnInit {
  form!: FormGroup;

  clientList: IClient[] = [];
  client!: IClient;

  requestedQuoteLineList: IQuotationRequestLine[] = [];
  requestedQuoteLine!: IQuotationRequestLine;

  requestedQuoteList: IQuotationRequest[] = [];
  requestedQuote!: IQuotationRequest;

  serviceList: IService[] = [];
  service!: IService;


  services: IService[] = [];

  User: any;
  requestedQuoteDate= new Date();
  error_messages = {
    requestedQuoteDate: [
      { type: 'required', message: 'Date is required' },
    ],
    requestedQuoteDescription: [
      { type: 'required', message: 'A quote description is required' },
    ],
    userId: [
      { type: 'required', message: 'A User Name is required' },
    ]
  }

  constructor(
    private formBuilder: FormBuilder,
    private clientService: ClientService,
    private notificationService: NotificationsService,
    private adminService:AdminService
    //public userService: UserLoginService
  ) { }



  ngOnInit(): void {

    this.readServices();
    this.createForm();
   // this.readClients();
    //this.User = JSON.parse(localStorage.getItem("User"));
    console.log(this.User);
  }

  dropDown(serviceTypeName:any) {
    console.log("This is serviceTypeName", serviceTypeName)
    this.services = [];
    this.serviceList.forEach(y => {
      if (y.ServiceName == serviceTypeName) {
        this.services.push(y)
        console.log("This is service", y)
      }
    })
  }

  createForm() {
    this.form = this.formBuilder.group({
      Service_ID: ['', [Validators.required]],
      requestedQuoteDate: ['', [Validators.required]],
      requestedQuoteDescription: ['', [Validators.required]],
      ToAddress: ['', [Validators.required]],
      FromAddress: ['', [Validators.required]],
      Client_ID: ['', [Validators.required]]
     
    });
  }



  addServiceList: IService[] = [];

  CheckBox(ser: IService) {
    this.addServiceList.indexOf(ser) === -1 ? this.addServiceList.push(ser) : this.ClearCheckBox(this.addServiceList.indexOf(ser));
  }

  ClearCheckBox(serviceId: number) {
    this.addServiceList.splice(serviceId, 1);
  }

  readServices() {
    this.adminService.getAllService().subscribe((res) => {
      this.serviceList = res as IService[];
      console.log("this is serviceList", res);
    });
  }

  

 /* readClients() {
    this.clientService.getClients().subscribe((res) => {
      this.clientList = res as IClient[];
    })
  }*/


  OnSubmit() {
    if (this.form.valid) {
      const IQuotationRequest: IQuotationRequest = this.form.value;
      IQuotationRequest.QuotationRequestLine = this.form.controls['Service_ID'].value;
      console.log(IQuotationRequest, "Requested Quote");
      this.clientService.CreateRequestedQuote(IQuotationRequest).subscribe(res => {
        console.log(res, "this is res");
        for (let x = 0; x < IQuotationRequest.QuotationRequestLine.length; x++) {
          let IQuotationRequestLine: IQuotationRequestLine = {
            QuotationRequest_ID: res.requestedQuoteId, Service_ID: Number(IQuotationRequest.QuotationRequestLine[x]),
            QuotationRequestLine_ID: 0,
            QuotationReqLineDescription: ''
          }
          console.log(IQuotationRequestLine, "RequestedQuoteLine")
          this.clientService.CreateRequestedQuoteLine(IQuotationRequestLine).subscribe(tes => {
          })
        }
        this.refreshForm();
      },
        err => {
          if (err.status == 401) {
            this.notificationService.failToaster('Unable to Request Quote', 'Error');
          }
          else if (err.status != 201) {
            this.notificationService.errorToaster('Server Error, Please Contact System Administrator', 'Error');
          }
          else {
            console.log(err);
          }
        }
      );
    }
  }
  

  refreshForm() {
    this.requestedQuote = {
      QuotationRequest_ID: 0,
      Client_ID: 0,
      QuotationReqDescription: '',
      FromAddress:'',
      ToAddress:'',
      QuotationReqDate: null,
      QuotationRequestLine: [],
      Service_ID: 0,
    }
  }


}
