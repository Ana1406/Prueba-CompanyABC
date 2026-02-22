import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModalComment } from './modal-comment';

describe('ModalComment', () => {
  let component: ModalComment;
  let fixture: ComponentFixture<ModalComment>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModalComment]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ModalComment);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
