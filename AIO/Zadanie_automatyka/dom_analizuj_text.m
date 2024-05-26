open_system('dom_TExt.slx');

simTime = 52 * 7 * 24 * 3600; 
set_param('dom_TExt', 'StopTime', num2str(simTime));

simOut = sim('dom_TExt');

tSim = simOut.domSimData.time;
Text = simOut.domSimData.signals(1).values;

minTemp = min(Text);
avgTemp = mean(Text);
maxTemp = max(Text);

period = 52 * 7 * 24 * 3600; 
omega = 2 * pi / period;
phi = pi; 
amplitude = (maxTemp - minTemp) / 2;
baseline = (maxTemp + minTemp) / 2;

Text = baseline + amplitude * sin(omega * tSim + phi);

figure(1);
plot(tSim, Text, 'g', 'LineWidth', 2);
hold on;
plot(tSim, repmat(minTemp, length(tSim), 1), 'b', 'LineWidth', 2);
plot(tSim, repmat(maxTemp, length(tSim), 1), 'r', 'LineWidth', 2);
plot(tSim, repmat(avgTemp, length(tSim), 1), 'm', 'LineWidth', 2);
grid on;
xlabel('Time (s)');
ylabel('Temperature (°C)');
title('Annual Simulation of Outdoor Temperature');
legend('Outdoor Temperature', 'Minimum Temperature', 'Maximum Temperature', 'Average Temperature');
hold off;
