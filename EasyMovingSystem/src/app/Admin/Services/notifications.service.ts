import { Injectable } from '@angular/core';
import {ToastrService} from 'ngx-toastr'


@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private toastr: ToastrService) { }


  successToaster(message: string | undefined, title: string | undefined){
    this.toastr.success(message, title,{ positionClass: 'toast-top-center' })
  }

  failToaster(message: string | undefined, title: string | undefined){
    this.toastr.warning(message, title,{ positionClass: 'toast-top-center' })
  }

  errorToaster(message: string | undefined, title: string | undefined){
    this.toastr.error(message, title,{ positionClass: 'toast-top-center' })
  }

}