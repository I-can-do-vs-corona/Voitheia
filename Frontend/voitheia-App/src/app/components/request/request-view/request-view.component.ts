import { Component, OnInit, Inject } from '@angular/core';
import { RequestResponseDTO } from 'src/app/common/models/requestResponseDTO';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-request-view',
  templateUrl: './request-view.component.html',
  styleUrls: ['./request-view.component.scss']
})
export class RequestViewComponent implements OnInit {

  item: RequestResponseDTO;
  
  constructor(
    public dialogRef: MatDialogRef<RequestViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: RequestResponseDTO) {
      this.item = data["item"];
    }

  onCancel(): void {
    this.dialogRef.close(false);
  }

  onAccept(): void {
    this.dialogRef.close(true);
  }

  ngOnInit(): void {
  }

}
