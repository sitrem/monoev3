using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using MonoBrickFirmware.Native;


namespace MonoBrickFirmware.Movement
{	
	/// <summary>
	/// Class for EV3 motor
	/// </summary>
	public class Motor :  MotorBase
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MonoBrickFirmware.IO.Motor"/> class.
		/// </summary>
		/// <param name="port">Port.</param>
		public Motor(MotorPort port) : base()
		{
			this.BitField = MotorPortToBitfield(port);
			Reverse = false;
		}

		/// <summary>
		/// The reverse value
		/// </summary>
		protected bool reverse = false;
		
		/// <summary>
		/// Gets or sets a value indicating whether motor runs in reverse direction
		/// </summary>
		/// <value>
		/// <c>true</c> if reverse; otherwise, <c>false</c>.
		/// </value>
		public bool Reverse {
			get {
				return reverse;
			} 
			set {
				reverse = value;
				if(reverse){
					output.SetPolarity(Polarity.Backward);
				}
				else{
					output.SetPolarity(Polarity.Forward);
				}
			}
		}
		
		/// <summary>
		/// Move the motor
		/// </summary>
		/// <param name='speed'>
		/// Speed of the motor -100 to 100
		/// </param>
		public void SetSpeed(sbyte speed)
		{
			CancelPolling();
			output.Start(speed);
		}
		
		/// <summary>
		/// Create a speed profile where ramp up and down is specified in steps
		/// </summary>
		/// <param name="speed">Maximum speed of the motor.</param>
		/// <param name="rampUpSteps">Ramp up steps.</param>
		/// <param name="constantSpeedSteps">Constant speed steps.</param>
		/// <param name="rampDownSteps">Ramp down steps.</param>
		/// <param name="brake">If set to <c>true</c> the motor will brake when movement is done.</param>
		/// <param name='waitForCompletion'>
		/// Set to <c>true</c> to wait for movement to be completed before returning
		/// </param>
		public void SpeedProfileStep (sbyte speed, UInt32 rampUpSteps, UInt32 constantSpeedSteps, UInt32 rampDownSteps, bool brake, bool waitForCompletion = true)
		{
			if(!waitForCompletion)
				CancelPolling();
			output.SetPower (0);
			output.SetStepSpeed (speed, rampUpSteps, constantSpeedSteps, rampDownSteps, brake);
			if (waitForCompletion) 
				WaitForMotorsToStartAndStop();
		}
		
		/// <summary>
		/// Create a speed profile where ramp up and down is specified in time
		/// </summary>
		/// <param name="speed">Maximum speed of the motor.</param>
		/// <param name="rampUpTimeMs">Ramp up time ms.</param>
		/// <param name="constantSpeedTimeMs">Constant speed time ms.</param>
		/// <param name="rampDownTimeMs">Ramp down time ms.</param>
		/// <param name="brake">If set to <c>true</c> the motor will brake when movement is done.</param>
		/// <param name='waitForCompletion'>
		/// Set to <c>true</c> to wait for movement to be completed before returning
		/// </param>
		public void SpeedProfileTime(sbyte speed, UInt32 rampUpTimeMs, UInt32 constantSpeedTimeMs, UInt32 rampDownTimeMs, bool brake, bool waitForCompletion = true)
		{
			if(!waitForCompletion)
				CancelPolling();
			output.SetPower (0);
			output.SetTimeSpeed(speed, rampUpTimeMs, constantSpeedTimeMs, rampUpTimeMs, brake);
			if(waitForCompletion)
				WaitForMotorsToStartAndStop();
		}
		
		/// <summary>
		/// Create a power profile where ramp up and down is specified in steps
		/// </summary>
		/// <param name="power">Maximum power of the motor.</param>
		/// <param name="rampUpSteps">Ramp up steps.</param>
		/// <param name="constantSpeedSteps">Constant speed steps.</param>
		/// <param name="rampDownSteps">Ramp down steps.</param>
		/// <param name="brake">If set to <c>true</c> the motor will brake when movement is done.</param>
		/// <param name='waitForCompletion'>
		/// Set to <c>true</c> to wait for movement to be completed before returning
		/// </param>
		public void PowerProfileStep(sbyte power, UInt32 rampUpSteps, UInt32 constantSpeedSteps, UInt32 rampDownSteps, bool brake, bool waitForCompletion = true)
		{
			if(!waitForCompletion)
				CancelPolling();
			output.SetPower (0);
			output.SetStepPower(power,rampUpSteps, constantSpeedSteps, rampDownSteps, brake);
			if(waitForCompletion)
				WaitForMotorsToStartAndStop();
		}
		
		/// <summary>
		/// Create a power profile where ramp up and down is specified in time
		/// </summary>
		/// <param name="power">Maximum power of the motor.</param>
		/// <param name="rampUpTimeMs">Ramp up time ms.</param>
		/// <param name="constantSpeedTimeMs">Constant speed time ms.</param>
		/// <param name="rampDownTimeMs">Ramp down time ms.</param>
		/// <param name="brake">If set to <c>true</c> the motor will brake when movement is done.</param>
		public void PowerProfileTime (byte power, UInt32 rampUpTimeMs, UInt32 constantSpeedTimeMs, UInt32 rampDownTimeMs, bool brake, bool waitForCompletion = true)
		{
			if(!waitForCompletion)
				CancelPolling();
			output.SetPower (0);
			output.SetTimePower(power, rampUpTimeMs,constantSpeedTimeMs,rampDownTimeMs, brake);
			if(waitForCompletion)
				WaitForMotorsToStartAndStop();
			
		}

		/// <summary>
		/// Resets the tacho
		/// </summary>
		public void ResetTacho()
		{
			output.ClearCount();
		}
	
		/// <summary>
		/// Gets the tacho count.
		/// </summary>
		/// <returns>
		/// The tacho count
		/// </returns>
	    public Int32 GetTachoCount(){
			return output.GetCount(this.PortList[0]);
		}
		
		/// <summary>
		/// Gets the speed of the motor
		/// </summary>
		/// <returns>The speed.</returns>
		public sbyte GetSpeed ()
		{
			return output.GetSpeed(this.PortList[0]);
		}
	}
}