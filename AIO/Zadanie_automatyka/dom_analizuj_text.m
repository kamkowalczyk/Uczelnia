relayOut = simOut.domSimData.signals(3).values;

% Stan z 0 na 1
onIndices = find(diff(relayOut) == 1) + 1;
% Stan z 1 na 0
offIndices = find(diff(relayOut) == -1) + 1;

% Czas trwania każdego stanu wysokiego:
highStateDurations = simOut.domSimData.time(offIndices) - simOut.domSimData.time(onIndices);

% Suma czasów stanów wysokich:
totalHighStateDuration = sum(highStateDurations);

% Wynik:
disp(['Total high state duration: ', num2str(totalHighStateDuration), ' seconds']);
