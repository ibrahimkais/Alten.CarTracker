import {
    MatButtonModule,
    MatExpansionModule,
    MatSidenavModule,
    MatIconModule,
    MatTableModule,
    MatSelectModule,
    MatToolbarModule,
    MatDividerModule,
    MatFormFieldModule,
    MatTooltipModule,
} from '@angular/material';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import {ScrollDispatchModule} from '@angular/cdk/scrolling';

@NgModule({
    imports: [
        MatButtonModule,
        MatExpansionModule,
        MatSidenavModule,
        MatIconModule,
        MatTableModule,
        MatSelectModule,
        MatToolbarModule,
        ReactiveFormsModule,
        MatDividerModule,
        MatFormFieldModule,
        MatTooltipModule,
        FormsModule,
        ScrollDispatchModule
    ],
    exports: [
        MatButtonModule,
        MatExpansionModule,
        MatSidenavModule,
        MatIconModule,
        MatTableModule,
        MatSelectModule,
        MatToolbarModule,
        ReactiveFormsModule,
        MatDividerModule,
        MatFormFieldModule,
        MatTooltipModule,
        FormsModule,
        ScrollDispatchModule
    ],
})

export class MaterialModule { }
