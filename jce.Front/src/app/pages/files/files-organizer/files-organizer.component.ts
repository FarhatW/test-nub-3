import {Component, OnInit} from '@angular/core';
import {FilesService} from "../../../@core/data/services/files.service";
import {SelectItem, TreeNode} from "primeng/primeng";
import {IDTONode} from "../../../@core/data/models/files/DTONode";
import {environment} from "../../../../environments/environment";
import {NotificationService} from "../../../@core/data/services/notification.service";
import {ToasterService} from "angular2-toaster";

@Component({
  selector: 'ngx-files-organizer',
  templateUrl: './files-organizer.component.html',
  styleUrls: ['./files-organizer.component.scss']
})
export class FilesOrganizerComponent implements OnInit {



  errorMessage: string;
  fileList: TreeNode[];
  selectedNodes: TreeNode;
  foldersDropdown: SelectItem[] = [];
  selectedFolder: IDTONode;
  showCreateFolderPopup: boolean = false;
  NewFolderName: string = '';
  url: string = this.filesService.uploadsEndPoint;

  constructor(private filesService: FilesService,
              private toasterService: ToasterService,
              private notificationService: NotificationService) {
  }

  ngOnInit() {

    this.getFilesAndFolders();
  }

  public getFilesAndFolders() {
    this.errorMessage = '';

    //Clear Filelist
    this.fileList = [];

    // Call the service -- to get Files
    this.filesService.getFiles()
      .subscribe((files) => {
          // Show the Files in the Tree Control
          this.fileList = files.children;
        },
        error => this.errorMessage = <any>error);

    // Clear the foldersDropdown
    this.foldersDropdown = [];

    // Call the service -- to get Folders
    this.filesService.getFolders()
      .subscribe((folders) => {
          var tempFoldersDropdown: SelectItem[] = [];

          // Loop through the returned Nodes
          for (let folder of folders.children) {
            // Create a new SelectedItem
            let newSelectedItem: SelectItem = {
              label: folder.label,
              value: folder.data
            }
            if (newSelectedItem.label !== 'JCEBase') {
              tempFoldersDropdown.push(newSelectedItem);
            }
            // Add Selected Item to the DropDown
          }

          // Add items to the DropDown
          this.foldersDropdown = tempFoldersDropdown;
           // this.selectedFolder = this.foldersDropdown;

          // Set the selected option to the first option
           this.selectedFolder = this.foldersDropdown[0].value;
        },
        error => this.errorMessage = <any>error);
  }

  public deleteItems() {

    let ParentDTONode: IDTONode = {
      data: '',
      label: '',
      expandedIcon: '',
      collapsedIcon: '',
      children: [],
      parentId: 0
    }

    if (this.selectedNodes.type !== 'folder'){

      let ChildDTONode: IDTONode = {
        data: this.selectedNodes.data,
        label: this.selectedNodes.label,
        expandedIcon: this.selectedNodes.expandedIcon,
        collapsedIcon: this.selectedNodes.collapsedIcon,
        children: [],
        parentId: 0
      }

      ParentDTONode.children.push(ChildDTONode);

      this.filesService.deleteFilesAndFolders(ParentDTONode)
        .subscribe(() => {
            // Refresh the files and folders
            this.getFilesAndFolders();
          },
          error => this.errorMessage = <any>error);

    } else {
      console.log('pas passé.')
      const title: string = 'Attention !';
      const body: string = 'Vous ne pouvez pas supprimer le dossier ' + this.selectedNodes.label + '.';
      this.toasterService.popAsync(
        this.notificationService.showWarningToast(title, body));
    }
  }

  public openCreateFolder() {
    // Open Popup
    this.showCreateFolderPopup = true;
  }

  public CreateFolder() {

    // Construct the new folder
    var NewFolder: string = this.selectedFolder + '\\' + this.NewFolderName;

    // Call the service
    this.filesService.createFolder(NewFolder)
      .subscribe(res => {

          // Refresh the files and folders
          this.getFilesAndFolders();
          console.log('res', res);

          // Close popup
          this.showCreateFolderPopup = false;
        },
        error => {
          this.errorMessage = <any>error;
          console.log('errror', error);
        }
      )
  }

  public onBeforeUploadHandler(event) {
    // called before the file(s) are uploaded
    // Send the currently selected folder in the Header
    console.log('eventformdata', event.formData);
    event.formData.append('selectedFolder', this.selectedFolder);
  }

  public onUploadHandler(event) {
    console.log('event', event);
    // Called after the file(s) are upladed
    // Refresh the files and folders
    console.log('evenstatus', event.xhr.status)

    if (event.xhr.status === 200) {

      event.files.forEach(item => {
        this.toasterService.popAsync(
          this.notificationService.showSuccessToast(
            'Image ajoutée', item.name))
      })
    }
    event.formData.remove('selectedFolder', null);

    this.getFilesAndFolders();
  }
}
