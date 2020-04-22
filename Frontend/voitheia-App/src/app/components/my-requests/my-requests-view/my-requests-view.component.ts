import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MyRequestDTO } from 'src/app/common/models/myRequestDTO';

@Component({
  selector: 'app-my-requests-view',
  templateUrl: './my-requests-view.component.html',
  styleUrls: ['./my-requests-view.component.scss']
})
export class MyRequestsViewComponent implements OnInit {

  item: MyRequestDTO;
  
  constructor(
    public dialogRef: MatDialogRef<MyRequestsViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MyRequestDTO) {
      this.item = data["item"];
    }

  onCancelRequest(): void {
    this.dialogRef.close("Cancel");
  }

  onCloseRequest(): void {
    this.dialogRef.close("Close");
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  ngOnInit(): void {
  }
}
