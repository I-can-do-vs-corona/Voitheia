import { Component, OnInit, Input, Inject } from '@angular/core';
import { RequestResponseDTO } from 'src/app/common/models/requestResponseDTO';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-my-requests-view',
  templateUrl: './my-requests-view.component.html',
  styleUrls: ['./my-requests-view.component.scss']
})
export class MyRequestsViewComponent implements OnInit {

  item: RequestResponseDTO;
  
  constructor(
    public dialogRef: MatDialogRef<MyRequestsViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: RequestResponseDTO) {
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
