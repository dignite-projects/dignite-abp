import { NgModule } from '@angular/core';
import { Router, RouterModule, Routes } from '@angular/router';
import { FileDomeComponent } from './components/dome/file-dome.component';

const routes: Routes = [
  {
    path: 'file-dome',
    component: FileDomeComponent
},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FileExplorerRoutingModule { 
  
}
