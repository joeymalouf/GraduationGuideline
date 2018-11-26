import { Injectable } from '@angular/core';

@Injectable()
export class NavService {

    public steps: Steps[];
    constructor() {

    }

    toggleStep(stepName: string) {
        this.steps.find(x => x.stepName == stepName).status = !this.steps.find(x => x.stepName == stepName).status;
    }

    setSteps(steps: Steps[]){
        this.steps = steps;
    }
}

interface Steps {
    username: string;
    status: boolean;
    stepName: string;
}