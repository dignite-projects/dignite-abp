import { Component, OnInit } from '@angular/core';
import { FormConfigLoaderService } from './serves/form-config-loader.service';

@Component({
  selector: 'app-root',
  template: `
    <abp-loader-bar></abp-loader-bar>
    <abp-dynamic-layout></abp-dynamic-layout>
    <abp-internet-status></abp-internet-status>
  `,
})
export class AppComponent implements OnInit{
  constructor(  private _FormConfigLoaderService: FormConfigLoaderService) {}
  async ngOnInit(): Promise<void> {
    //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
    //Add 'implements OnInit' to the class.
   await this._FormConfigLoaderService.loadConfig();
  }
}
