import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';
import {FilesComponent} from './files.component';
import {FilesOrganizerComponent} from './files-organizer/files-organizer.component';


const routes: Routes = [{
  path: '',
  component: FilesComponent,
  children: [
    {
      path: '',
      component: FilesOrganizerComponent
    }
  ],
}];

@NgModule({
  imports: [
    RouterModule.forChild(routes),
  ],
  exports: [
    RouterModule,
  ],
})
export class FilesRoutingModule {

}

export const routedComponents = [
];
